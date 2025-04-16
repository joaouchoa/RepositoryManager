using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Domain.Utils;
using MediatR;

namespace ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public class GetRepoByNameQueryHandler : IRequestHandler<GetRepoByNameQuery, Result<GetRepoByNameQueryResponse>>
    {
        private readonly GetRepoByNameQueryValidation _validator;
        private readonly IReposReadRepository _repository;

        public GetRepoByNameQueryHandler(IReposReadRepository repository, GetRepoByNameQueryValidation validator)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<Result<GetRepoByNameQueryResponse>> Handle(GetRepoByNameQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
                return Result<GetRepoByNameQueryResponse>.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());

            var response = await _repository.GetByRepositoryByNameAsync(request.RepoName, request.Page, request.PerPage, request.SortBy);

            if (response is null )
                return Result<GetRepoByNameQueryResponse>.NotFound("No repositories found.");

            var externalRepoIds = response.Repositories.Select(r => r.Id).ToList();

            var favoriteRepoIds = await _repository.GetFavoriteRepositoriesAsync(externalRepoIds);

            var repositories = response.Repositories.Select(repo => new Repo
            {
                Id = repo.Id,
                Name = repo.Name,
                Description = repo.Description,
                Url = repo.Url,
                Language = repo.Language,
                Owner = repo.Owner.Login,
                Stargazers = repo.StargazersCount,
                Forks = repo.ForksCount,
                Watchers = repo.WatchersCount,
                Favorited = favoriteRepoIds.Contains(repo.Id)
            }).ToList();

            var result = new GetRepoByNameQueryResponse(
                finalPage: response.TotalPages,
                Repositories: repositories,
                Errors: null
            );

            return Result<GetRepoByNameQueryResponse>.Sucess(result);
        }
    }
}