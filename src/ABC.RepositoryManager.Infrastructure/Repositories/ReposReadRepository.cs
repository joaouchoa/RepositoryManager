using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Application.Features.Repositories.DTOs;
using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Domain.Enums;
using ABC.RepositoryManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ABC.RepositoryManager.Infrastructure.Repositories
{
    public class ReposReadRepository : IReposReadRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ReposManagerContext _context;

        public ReposReadRepository(HttpClient httpClient, ReposManagerContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<bool> ExistsFavoriteRepoByIdAsync(long id)
        {
            return await _context.Repos.AnyAsync(e => e.Id == id);
        }

        public async Task<GetRepoByNameGitHubResponse> GetByRepositoryByNameAsync(string repoName, int page, int perPage, ERepoSortBy? sortBy)
        {
            var sort = sortBy switch
            {
                ERepoSortBy.Stars => "stars",
                ERepoSortBy.Forks => "forks",
                ERepoSortBy.Updated => "updated",
                _ => null
            };

            var url = $"https://api.github.com/search/repositories?q={repoName}&page={page}&per_page={perPage}";

            if (!string.IsNullOrEmpty(sort))
                url += $"&sort={sort}&order=desc";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("RepoManager", "1.0"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonSerializer.Deserialize<GetRepoByNameGitHubResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (body is null || body.Repositories is null || !body.Repositories.Any())
                return new GetRepoByNameGitHubResponse(0, new List<RepositoryGitHubResponse>(), page);

            // Por padrão o Github já tem a sua relevância que considera o match do nome e numero de estrelas como prioridade alta,
            // popularidade do dono do repositorio e frequência de atualizações como prioridade média e quantidade forks como prioridade baixa,
            // por exemplo. Então vou aplicar uma especificação de prioridade considerando esse fator padrão do github na busca de repositorios,
            // quando for uma busca sem filtros, ou seja, o Enum ERepoSortBy vier null, vou 
            // definir notas de peso para algumas carateriticas fornecidade pela API publica, faço a soma desses pessos, assim gerando uma nota para
            // os repositorios, depois ordenando eles com base nessa nota. Entendo que não é extremamente preciso, pois estou aplicando o calculo
            // somente no arranjo amostral da paginação, mas acredito que seja um alternativa boa para o requisito.
            if (sortBy == null)
            {
                // Calculo de relevância
                // Estrelas: peso 2.0 
                // Forks: peso 1.5 
                // Watchers: peso 1.0 

                body = body with
                {
                    Repositories = body.Repositories
                        .OrderByDescending(r =>
                            (r.StargazersCount * 2.0) +
                            (r.ForksCount * 1.5) +
                            (r.WatchersCount * 1.0)
                        ).ToList()
                };
            }

            response.Headers.TryGetValues("Link", out var linkHeaders);
            var linkHeader = linkHeaders?.FirstOrDefault();

            var lastPage = ExtractLastPageNumber(linkHeader, page);

            return body with
            {
                TotalPages = lastPage
            };
        }

        public async Task<List<Repo>> GetFavoriteReposAsync(int page, int perPage, ERepoSortBy? SortBy)
        {
            var query = _context.Repos.AsQueryable();

            if (SortBy is null)
            {
                // Calcula a "nota de relevância" baseada nos pesos, semelhante ao da listagem por nome, a diferença 
                // é que aqui como tenho ciencia de todos os registros eu posso aplicar a relevancia para todos os registros.
                query = query
                    .OrderByDescending(r =>
                        (r.Stargazers * 2.0) +
                        (r.Forks * 1.5) +
                        (r.Watchers * 1.0)
                    );
            }
            else
            {
                query = SortBy switch
                {
                    ERepoSortBy.Stars => query.OrderByDescending(r => r.Stargazers),
                    ERepoSortBy.Forks => query.OrderByDescending(r => r.Forks),
                    _ => query.OrderByDescending(r => r.Stargazers)
                };
            }

            return await query
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();
        }

        public async Task<int> GetFavoriteReposCountAsync()
        {
            return await _context.Repos.CountAsync();
        }

        private int ExtractLastPageNumber(string linkHeader, int currentPage)
        {
            if (string.IsNullOrWhiteSpace(linkHeader))
                return currentPage; 

            var links = linkHeader.Split(',');

            foreach (var link in links)
            {
                if (link.Contains("rel=\"last\""))
                {
                    var lastPageUrl = link.Substring(0, link.IndexOf(";")).Trim('<', '>', ' ');
                    var pageParam = System.Web.HttpUtility
                        .ParseQueryString(new Uri(lastPageUrl).Query)
                        .Get("page");

                    return int.TryParse(pageParam, out var page) ? page : currentPage;
                }
            }

            return currentPage;
        }
    }
}
