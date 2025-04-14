using ABC.RepositoryManager.Domain.Enums;
using ABC.RepositoryManager.Domain.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.RepositoryManager.Application.Features.Repositories.Queries.GetFavoriteRepos
{
    public record GetFavoriteReposQuery(int page, int perPage, ERepoSortBy? SortBy) : IRequest<Result<GetFavoriteReposQueryResponse>>;
}
