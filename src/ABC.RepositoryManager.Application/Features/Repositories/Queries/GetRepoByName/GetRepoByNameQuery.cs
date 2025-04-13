using ABC.RepositoryManager.Domain.Enums;
using MediatR;

namespace ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public record GetRepoByNameQuery(string RepoName, int Page, int PerPage, ERepoSortBy SortBy) : IRequest<GetRepoByNameQueryResponse>;
}
