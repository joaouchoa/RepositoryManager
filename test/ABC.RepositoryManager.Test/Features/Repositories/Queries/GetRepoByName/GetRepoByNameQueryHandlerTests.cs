using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Application.Features.Repositories.DTOs;
using ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName;
using ABC.RepositoryManager.Test.Fakers;
using FluentAssertions;
using NSubstitute;
using System.Net;

namespace ABC.RepositoryManager.Test.Features.Repositories.Queries.GetRepoByName
{
    public class GetRepoByNameQueryHandlerTests
    {
        private readonly IReposReadRepository _repoRepository;
        private readonly GetRepoByNameQueryHandler _handler;

        public GetRepoByNameQueryHandlerTests()
        {
            _repoRepository = Substitute.For<IReposReadRepository>();
            var validator = new GetRepoByNameQueryValidation();
            _handler = new GetRepoByNameQueryHandler(_repoRepository, validator);
        }

        [Fact]
        public async Task Handle_Should_Return_Repositories_With_Favorited_True()
        {
            // Arrange
            var query = new GetRepoByNameQuery("TestRepo", 1, 10, null);
            var fakeRepo = RepositoryGitHubResponseFaker.Generate(id: 123);

            _repoRepository
                .GetByRepositoryByNameAsync("TestRepo", 1, 10, null)
                .Returns(Task.FromResult(
                    new GetRepoByNameGitHubResponse(
                        TotalCount: 1,
                        Repositories: new List<RepositoryGitHubResponse> { fakeRepo },
                        TotalPages: 1
                    )
                ));

            _repoRepository.GetFavoriteRepositoriesAsync(Arg.Any<List<long>>())
                .Returns(new List<long> { 123 }); // Mock: repo é favorito

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Resultado.Should().NotBeNull();
            result.Resultado.Repositories.Should().HaveCount(1);
            result.Resultado.Repositories[0].Favorited.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_Return_Repositories_With_Favorited_False_When_Not_In_Database()
        {
            // Arrange
            var query = new GetRepoByNameQuery("TestRepo", 1, 10, null);
            var fakeRepo = RepositoryGitHubResponseFaker.Generate(id: 456);

            _repoRepository.GetByRepositoryByNameAsync("TestRepo", 1, 10, null)
                .Returns(Task.FromResult(new GetRepoByNameGitHubResponse(
                    1,
                    new List<RepositoryGitHubResponse> { fakeRepo },
                    1)));

            _repoRepository.GetFavoriteRepositoriesAsync(Arg.Any<List<long>>())
                .Returns(new List<long>()); // Nenhum favorito

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Resultado.Repositories[0].Favorited.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_Should_Return_OK_When_No_Repositories_Found()
        {
            // Arrange
            var query = new GetRepoByNameQuery("EmptyRepo", 1, 10, null);

            _repoRepository.GetByRepositoryByNameAsync("EmptyRepo", 1, 10, null)
                .Returns(Task.FromResult(new GetRepoByNameGitHubResponse(0, new List<RepositoryGitHubResponse>(), 0)));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Resultado.Repositories.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_Should_Return_BadRequest_When_Validation_Fails()
        {
            // Arrange
            var invalidQuery = new GetRepoByNameQuery("", 0, 0, null); // nome vazio, pagina inválida

            // Act
            var result = await _handler.Handle(invalidQuery, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Resultado.Should().BeNull();
            result.Erros.Should().NotBeEmpty();
        }
    }
}
