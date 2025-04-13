using ABC.RepositoryManager.Domain.Entities;

namespace ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public record GetRepoByNameQueryResponse(
           int finalPage,
           List<Repo> Repositories,
           List<string> Errors
    );
}