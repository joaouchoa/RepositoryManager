using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Application.Features.Repositories.DTOs;
using ABC.RepositoryManager.Domain.Enums;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ABC.RepositoryManager.Infrastructure.Repositories
{
    public class ReposReadRepository : IReposReadRepository
    {
        private readonly HttpClient _httpClient;

        public ReposReadRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

            response.Headers.TryGetValues("Link", out var linkHeaders);
            var linkHeader = linkHeaders?.FirstOrDefault();

            var lastPage = ExtractLastPageNumber(linkHeader, page);

            return body with
            {
                TotalPages = lastPage
            };
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
