using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Domain.Enums;
using ABC.RepositoryManager.Domain.Utils;
using MediatR;

namespace ABC.RepositoryManager.Application.Features.Repositories.Commands.CreateFavoriteRepo
{
    public class CreateFavoriteRepoCommandHandler : IRequestHandler<CreateFavoriteRepoCommand, Result>
    {
        private readonly IReposWriteRepository _repository;
        private readonly CreateFavoriteRepoCommandValidator _validator;

        public CreateFavoriteRepoCommandHandler(IReposWriteRepository repository, CreateFavoriteRepoCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result> Handle(CreateFavoriteRepoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Result.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
            }

            var favorite = new Repo(
                request.Id,
                request.Name,
                request.Description,
                request.Url,
                request.Language,
                request.Owner,
                request.Stargazers,
                request.Forks,
                request.Watchers
            );

            var result = await _repository.CreateFavoriteRepo(favorite);

            if (result != ERepoCreationStatus.Success)
            {
                return Result.BadRequest(result.ToMessage());
            }

            return Result.Created();
        }
    }
}
