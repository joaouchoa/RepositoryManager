using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Application.Features.Repositories.DTOs;
using ABC.RepositoryManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.RepositoryManager.Infrastructure.Repositories
{
    public class ReposReadRepository : IReposReadRepository
    {
        public Task<GetRepoByNameGitHubResponse> GetByRepositoryByNameAsync(string? repoName, int page, int perPage, ERepoSortBy SortBy)
        {
            throw new NotImplementedException();
        }
    }
}
