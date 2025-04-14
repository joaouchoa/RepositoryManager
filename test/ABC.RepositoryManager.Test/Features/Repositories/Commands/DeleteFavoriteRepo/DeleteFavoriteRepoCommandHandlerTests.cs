using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Application.Features.Repositories.Commands.CreateFavoriteRepo;
using ABC.RepositoryManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ABC.RepositoryManager.Test.Features.Repositories.Commands.DeleteFavoriteRepo
{
    public class DeleteFavoriteRepoCommandHandlerTests
    {
        private readonly IReposWriteRepository _writeRepo;
        private readonly DeleteFavoriteRepoCommandHandler _handler;

        public DeleteFavoriteRepoCommandHandlerTests()
        {
            _writeRepo = Substitute.For<IReposWriteRepository>();
            var validator = new DeleteFavoriteRepoCommandValidator();
            _handler = new DeleteFavoriteRepoCommandHandler(_writeRepo, validator);
        }

        [Fact]
        public async Task Handle_Should_Return_NoContent_When_Deletion_Is_Successful()
        {
            // Arrange
            var command = new DeleteFavoriteRepoCommand(123);

            _writeRepo.DeleteFavoriteRepoAsync(123).Returns(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_Repo_Does_Not_Exist()
        {
            // Arrange
            var command = new DeleteFavoriteRepoCommand(123);

            _writeRepo.DeleteFavoriteRepoAsync(123).Returns(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Repository_Fails()
        {
            // Arrange
            var command = new DeleteFavoriteRepoCommand(123);

            _writeRepo.DeleteFavoriteRepoAsync(123)
                .Throws(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
