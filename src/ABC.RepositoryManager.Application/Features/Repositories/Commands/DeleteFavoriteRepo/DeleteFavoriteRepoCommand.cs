using ABC.RepositoryManager.Domain.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.RepositoryManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo
{
    public record DeleteFavoriteRepoCommand(int Id) : IRequest<Result>;
}
