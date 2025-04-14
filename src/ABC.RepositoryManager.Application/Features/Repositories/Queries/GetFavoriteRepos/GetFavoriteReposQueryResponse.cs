using ABC.RepositoryManager.Domain.Entities;

namespace ABC.RepositoryManager.Application.Features.Repositories.Queries.GetFavoriteRepos
{
    public record GetFavoriteReposQueryResponse(
           int finalPage,
           List<Repo> Repositories,
           List<string> Errors
    );
}