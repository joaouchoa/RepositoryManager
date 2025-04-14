using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.RepositoryManager.Infrastructure.Repositories
{
    public class ReposWriteRepository : IReposWriteRepository
    {
        public Task<ERepoCreationStatus> CreateFavoriteRepo(Repo repository)
        {
            throw new NotImplementedException();
        }
    }
}
