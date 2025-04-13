using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Domain.Entities;
using MediatR;

namespace ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName
{
    public class GetRepoByNameQueryHandler : IRequestHandler<GetRepoByNameQuery, GetRepoByNameQueryResponse>
    {
        private readonly GetRepoByNameQueryValidation _validator;
        private readonly IReposReadRepository _repository;

        public GetRepoByNameQueryHandler(IReposReadRepository repository, GetRepoByNameQueryValidation validator)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<GetRepoByNameQueryResponse> Handle(GetRepoByNameQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            
            if (!validationResult.IsValid)
                return new GetRepoByNameQueryResponse(false, default, default, validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            var response = await _repository.GetByRepositoryByNameAsync(request.RepoName, request.Page, request.PerPage, request.SortBy);

            if (response is null || response.Repositories.Count == 0)
                return new GetRepoByNameQueryResponse(
                    HasMorePages: false,
                    finalPage: 0,
                    Repositories: new List<Repo>(),
                    Errors: null
                );

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
                Watchers = repo.WatchersCount
            }).ToList();

            return new GetRepoByNameQueryResponse(response.HasMorePages, response.TotalPages, repositories, default);
        }
    }
}
