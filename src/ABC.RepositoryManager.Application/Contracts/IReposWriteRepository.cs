﻿using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Domain.Enums;

namespace ABC.RepositoryManager.Application.Contracts
{
    public interface IReposWriteRepository
    {
        Task<ERepoCreationStatus> CreateFavoriteRepoAsync(Repo repository);
        Task<bool> DeleteFavoriteRepoAsync(int id);
    }
}
