using ABC.RepositoryManager.Domain.Entities;

namespace ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public record GetRepoByNameQueryResponse(
           bool HasMorePages,
           int finalPage,
           List<Repo> Repositories,
           List<string> Errors
    );
}