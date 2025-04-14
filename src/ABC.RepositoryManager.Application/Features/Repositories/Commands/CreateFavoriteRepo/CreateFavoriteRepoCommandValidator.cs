using ABC.RepositoryManager.Application.ValidationMessages;
using ABC.RepositoryManager.Domain.Entities;
using FluentValidation;

namespace ABC.RepositoryManager.Application.Features.Repositories.Commands.CreateFavoriteRepo
{
    public class CreateFavoriteRepoCommandValidator : AbstractValidator<CreateFavoriteRepoCommand>
    {
        public CreateFavoriteRepoCommandValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE)
                .GreaterThan(0).WithMessage(RepoValidationMessages.ID_MATCHES_ERROR_MESSAGE);

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE)
                .MaximumLength(Repo.MAX_LENGHT).WithMessage(RepoValidationMessages.MAX_LENGTH_ERROR_MESSAGE)
                .Must(name => !name.StartsWith("-") && !name.EndsWith("-"))
                .WithMessage(RepoValidationMessages.START_END_HYPHEN_ERROR_MESSAGE)
                .Must(name => !name.Contains("--"))
                .WithMessage(RepoValidationMessages.CONSECUTIVE_HYPHENS_ERROR_MESSAGE);

            RuleFor(d => d.Url)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE);

            RuleFor(d => d.Watchers)
                .GreaterThanOrEqualTo(0).WithMessage(RepoValidationMessages.NEGATIVE_NUMBER_ERROR_MESSAGE);

            RuleFor(d => d.Forks)
                .GreaterThanOrEqualTo(0).WithMessage(RepoValidationMessages.NEGATIVE_NUMBER_ERROR_MESSAGE);

            RuleFor(d => d.Stargazers)
                .GreaterThanOrEqualTo(0).WithMessage(RepoValidationMessages.NEGATIVE_NUMBER_ERROR_MESSAGE);
        }
    }
}