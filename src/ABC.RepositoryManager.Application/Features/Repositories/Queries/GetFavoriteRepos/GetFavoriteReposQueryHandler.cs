using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName;
using ABC.RepositoryManager.Domain.Enums;
using ABC.RepositoryManager.Domain.Utils;
using MediatR;
using System.Globalization;

namespace ABC.RepositoryManager.Application.Features.Repositories.Queries.GetFavoriteRepos
{
    public class GetFavoriteReposQueryHandler : IRequestHandler<GetFavoriteReposQuery, Result<GetFavoriteReposQueryResponse>>
    {
        private readonly IReposReadRepository _repository;
        private readonly GetFavoriteReposQueryValidator _validator;

        public GetFavoriteReposQueryHandler(IReposReadRepository repository, GetFavoriteReposQueryValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<GetFavoriteReposQueryResponse>> Handle(GetFavoriteReposQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
                return Result<GetFavoriteReposQueryResponse>.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());

            var repositorieCount = await _repository.GetFavoriteReposCountAsync();

            int totalPages = (int)Math.Ceiling((double)repositorieCount / request.perPage);
            int page = totalPages == 0 ? 1 : Math.Min(request.page, totalPages);

            var repositories = await _repository.GetFavoriteReposAsync(page, request.perPage, request.SortBy);

            if (repositories is null)
                return Result<GetFavoriteReposQueryResponse>.BadRequest("Repositories Not Found.");

            var result = new GetFavoriteReposQueryResponse(
                finalPage: totalPages,
                Repositories: repositories,
                Errors: null
            );

            return Result<GetFavoriteReposQueryResponse>.Sucess(result);
        }
    }
}
