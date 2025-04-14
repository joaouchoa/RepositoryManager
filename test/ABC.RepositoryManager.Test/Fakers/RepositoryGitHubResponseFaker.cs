using ABC.RepositoryManager.Application.Features.Repositories.DTOs;
using Bogus;

namespace ABC.RepositoryManager.Test.Fakers
{
    public static class RepositoryGitHubResponseFaker
    {
        public static RepositoryGitHubResponse Generate(long? id = null)
        {
            return new Faker<RepositoryGitHubResponse>()
                .RuleFor(r => r.Id, f => id ?? f.Random.Long(1, 999999))
                .RuleFor(r => r.Name, f => f.Internet.UserName())
                .RuleFor(r => r.Description, f => f.Lorem.Sentence())
                .RuleFor(r => r.Url, f => f.Internet.Url())
                .RuleFor(r => r.Language, f => f.PickRandom("C#", "Java", "Python", "Go", "JS"))
                .RuleFor(r => r.Owner, f => new OwnerGitHubResponse { Login = f.Internet.UserName() })
                .RuleFor(r => r.StargazersCount, f => f.Random.Int(0, 10000))
                .RuleFor(r => r.ForksCount, f => f.Random.Int(0, 1000))
                .RuleFor(r => r.WatchersCount, f => f.Random.Int(0, 1000))
                .Generate();
        }

        public static List<RepositoryGitHubResponse> GenerateList(int quantity)
        {
            return Enumerable.Range(1, quantity)
                .Select(_ => Generate())
                .ToList();
        }
    }
}
