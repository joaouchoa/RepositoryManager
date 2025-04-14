using ABC.RepositoryManager.Application.ValidationMessages;
using FluentValidation;

namespace ABC.RepositoryManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo
{
    public class DeleteFavoriteRepoCommandValidator : AbstractValidator<DeleteFavoriteRepoCommand>
    {
        public DeleteFavoriteRepoCommandValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage(RepoValidationMessages.ID_MATCHES_ERROR_MESSAGE);
        }
    }
}