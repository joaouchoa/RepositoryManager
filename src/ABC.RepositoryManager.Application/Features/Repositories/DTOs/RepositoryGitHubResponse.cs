using System.Text.Json.Serialization;

namespace ABC.RepositoryManager.Application.Features.Repositories.DTOs
{
    public record RepositoryGitHubResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("description")]
        public string Description { get; init; }

        [JsonPropertyName("html_url")]
        public string Url { get; init; }

        [JsonPropertyName("language")]
        public string Language { get; init; }

        [JsonPropertyName("owner")]
        public OwnerGitHubResponse Owner { get; init; }

        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; init; }

        [JsonPropertyName("forks_count")]
        public int ForksCount { get; init; }

        [JsonPropertyName("watchers_count")]
        public int WatchersCount { get; init; }
    }

}