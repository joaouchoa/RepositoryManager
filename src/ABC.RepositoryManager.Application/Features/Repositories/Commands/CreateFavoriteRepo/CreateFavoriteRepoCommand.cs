using ABC.RepositoryManager.Domain.Utils;
using MediatR;

namespace ABC.RepositoryManager.Application.Features.Repositories.Commands.CreateFavoriteRepo
{
    public record CreateFavoriteRepoCommand(
            long Id,
            string Name,
            string? Description,
            string Url,
            string? Language,
            string Owner,
            int Stargazers,
            int Forks,
            int Watchers) : IRequest<Result>;
}
