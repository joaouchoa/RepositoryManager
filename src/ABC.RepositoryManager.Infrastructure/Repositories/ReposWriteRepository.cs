using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Domain.Enums;
using ABC.RepositoryManager.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.RepositoryManager.Infrastructure.Repositories
{
    public class ReposWriteRepository : IReposWriteRepository
    {
        private readonly IReposReadRepository _reposReadRepository;
        private readonly ReposManagerContext _context;

        public ReposWriteRepository(IReposReadRepository reposReadRepository, ReposManagerContext context)
        {
            _reposReadRepository = reposReadRepository;
            _context = context;
        }

        public async Task<ERepoCreationStatus> CreateFavoriteRepo(Repo repository)
        {
            var exists = await _reposReadRepository.ExistsFavoriteRepoByIdAsync(repository.Id);

            if (exists)
                return ERepoCreationStatus.AlreadyExists;

            await _context.Repos.AddAsync(repository);
            var success = await _context.SaveChangesAsync() > 0;

            if (!success)
                return ERepoCreationStatus.Failure;

            return ERepoCreationStatus.Success;
        }
    }
}
