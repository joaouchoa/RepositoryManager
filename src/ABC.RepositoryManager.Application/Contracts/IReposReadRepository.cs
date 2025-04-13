using ABC.RepositoryManager.Application.Features.Repositories.DTOs;
using ABC.RepositoryManager.Domain.Enums;

namespace ABC.RepositoryManager.Application.Contracts
{
    public interface IReposReadRepository
    {
        Task<GetRepoByNameGitHubResponse> GetByRepositoryByNameAsync(string? repoName, int page, int perPage, ERepoSortBy? SortBy);
    }
}
