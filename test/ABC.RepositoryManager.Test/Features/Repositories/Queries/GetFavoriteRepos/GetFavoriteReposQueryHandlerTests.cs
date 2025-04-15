using System.Net;
using NSubstitute;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Application.Features.Repositories.Queries.GetFavoriteRepos;

namespace ABC.RepositoryManager.Test.Features.Repositories.Queries.GetFavoriteRepos
{
    public class GetFavoriteReposQueryHandlerTests
    {
        private readonly IReposReadRepository _repoRepository;
        private readonly GetFavoriteReposQueryHandler _handler;

        public GetFavoriteReposQueryHandlerTests()
        {
            _repoRepository = Substitute.For<IReposReadRepository>();
            var validator = new GetFavoriteReposQueryValidator();
            _handler = new GetFavoriteReposQueryHandler(_repoRepository, validator);
        }

        [Fact]
        public async Task Handle_Should_Return_Favorite_Repositories_When_Exists()
        {
            // Arrange
            var query = new GetFavoriteReposQuery(1, 10, null);

            var fakeRepos = new List<Repo>
            {
                new Repo(1, "Repo 1", "Desc", "url", "C#", "Joao", 10, 5, 2),
                new Repo(2, "Repo 2", "Desc", "url", "C#", "Maria", 20, 10, 5)
            };

            _repoRepository.GetFavoriteReposAsync(1, 10, null).Returns(fakeRepos);
            _repoRepository.GetFavoriteReposCountAsync().Returns(fakeRepos.Count);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Resultado.Repositories.Should().HaveCount(2);
        }

        [Fact]
        public async Task Handle_Should_Return_EmptyList_When_No_Repositories_Exist()
        {
            // Arrange
            var query = new GetFavoriteReposQuery(1, 10, null); 

            _repoRepository.GetFavoriteReposCountAsync().Returns(0); 
            _repoRepository.GetFavoriteReposAsync(1, 10, null).Returns(new List<Repo>()); 

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Resultado.Repositories.Should().BeEmpty();
            result.Resultado.finalPage.Should().Be(0);
        }


        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Repository_Fails()
        {
            // Arrange
            var query = new GetFavoriteReposQuery(1, 10, null);

            _repoRepository.GetFavoriteReposCountAsync().Returns(10);
            _repoRepository.GetFavoriteReposAsync(1, 10, null).Throws(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}