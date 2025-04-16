using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Domain.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.RepositoryManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo
{
    public class DeleteFavoriteRepoCommandHandler : IRequestHandler<DeleteFavoriteRepoCommand, Result>
    {
        private readonly IReposWriteRepository _repository;
        private readonly DeleteFavoriteRepoCommandValidator _validator;

        public DeleteFavoriteRepoCommandHandler(IReposWriteRepository repository, DeleteFavoriteRepoCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result> Handle(DeleteFavoriteRepoCommand request, CancellationToken cancellationToken)
        {
            var validatorResult = _validator.Validate(request);

            if (!validatorResult.IsValid)
                return Result.BadRequest(validatorResult.Errors.Select(e => e.ErrorMessage).ToArray());

            var response = await _repository.DeleteFavoriteRepoAsync(request.Id);

            if (!response)
                return Result.NotFound(ValidationMessages.RepoValidationMessages.FAVORITE_REPO_DONT_DELETED_ERROR_MESSAGE);

            return Result.Sucess();
        }
    }
}
