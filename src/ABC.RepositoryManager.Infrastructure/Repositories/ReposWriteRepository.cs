using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Domain.Enums;
using ABC.RepositoryManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteFavoriteRepo(int id)
        {
            var deletedCount = await _context.Repos
                 .Where(d => d.Id == id)
                 .ExecuteDeleteAsync();

            return deletedCount > 0;
        }
    }
}
