using Bogus;
using System.Net;
using NSubstitute;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using ABC.RepositoryManager.Domain.Enums;
using ABC.RepositoryManager.Domain.Entities;
using ABC.RepositoryManager.Application.Contracts;
using ABC.RepositoryManager.Application.Features.Repositories.Commands.CreateFavoriteRepo;

namespace ABC.RepositoryManager.Test.Features.Repositories.Commands.CreateFavoriteRepo
{
    public class CreateFavoriteRepoCommandHandlerTests
    {
        private readonly IReposWriteRepository _writeRepo;
        private readonly CreateFavoriteRepoCommandHandler _handler;
        private readonly Faker<CreateFavoriteRepoCommand> _faker;

        public CreateFavoriteRepoCommandHandlerTests()
        {
            _writeRepo = Substitute.For<IReposWriteRepository>();
            var validator = new CreateFavoriteRepoCommandValidator();
            _handler = new CreateFavoriteRepoCommandHandler(_writeRepo, validator);

            _faker = new Faker<CreateFavoriteRepoCommand>()
                .CustomInstantiator(f => new CreateFavoriteRepoCommand(
                    f.Random.Long(1, 999999),
                    f.Internet.UserName(),
                    f.Lorem.Sentence(),
                    f.Internet.Url(),
                    f.PickRandom("C#", "Java", "JS"),
                    f.Internet.UserName(),
                    f.Random.Int(1, 100),
                    f.Random.Int(1, 100),
                    f.Random.Int(1, 100)
                ));
        }

        [Fact]
        public async Task Handle_Should_Return_Created_When_Repo_Is_Saved()
        {
            // Arrange
            var command = _faker.Generate();

            _writeRepo.CreateFavoriteRepoAsync(Arg.Any<Repo>())
                .Returns(ERepoCreationStatus.Success);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Handle_Should_Return_BadRequest_When_Creation_Fails()
        {
            // Arrange
            var command = _faker.Generate();

            _writeRepo.CreateFavoriteRepoAsync(Arg.Any<Repo>())
                .Returns(ERepoCreationStatus.AlreadyExists);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Erros.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Handle_Should_Return_BadRequest_When_Command_Is_Invalid()
        {
            // Arrange
            var command = new CreateFavoriteRepoCommand(0, "", "", "", "", "", 0, 0, 0);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Erros.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Repository_Fails()
        {
            // Arrange
            var command = _faker.Generate();

            _writeRepo.CreateFavoriteRepoAsync(Arg.Any<Repo>())
                .Throws(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}