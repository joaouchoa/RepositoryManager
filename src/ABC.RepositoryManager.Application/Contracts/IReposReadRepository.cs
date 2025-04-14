using ABC.RepositoryManager.Application.Features.Repositories.DTOs;
using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Domain.Enums;

namespace ABC.RepositoryManager.Application.Contracts
{
    public interface IReposReadRepository
    {
        Task<GetRepoByNameGitHubResponse> GetByRepositoryByNameAsync(string? repoName, int page, int perPage, ERepoSortBy? SortBy);
        Task<bool> ExistsFavoriteRepoByIdAsync(long id);
        Task<int> GetFavoriteReposCountAsync();
        Task<List<Repo>> GetFavoriteReposAsync(int page, int perPage, ERepoSortBy? SortBy);
        Task<List<long>> GetFavoriteRepositoriesAsync(List<long> externalRepoIds);
    }
}
