using ABC.RepositoryManager.Application.ValidationMessages;
using FluentValidation;

namespace ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public class GetRepoByNameQueryValidation : AbstractValidator<GetRepoByNameQuery>
    {
        public GetRepoByNameQueryValidation()
        {
            RuleFor(d => d.RepoName)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE);

            RuleFor(d => d.Page)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE)
                .GreaterThan(0).WithMessage(RepoValidationMessages.NEGATIVE_NUMBER_ERROR_MESSAGE);

            RuleFor(d => d.PerPage)
                .NotEmpty().WithMessage(RepoValidationMessages.NOT_EMPTY_ERROR_MESSAGE)
                .GreaterThan(0).WithMessage(RepoValidationMessages.NEGATIVE_NUMBER_ERROR_MESSAGE)
                .LessThan(31).WithMessage(RepoValidationMessages.LIMITE_PER_PAGE_ERROR_MESSAGE);
        }
    }
}
