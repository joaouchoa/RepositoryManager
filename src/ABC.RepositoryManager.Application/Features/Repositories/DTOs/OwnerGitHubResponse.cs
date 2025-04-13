using System.Text.Json.Serialization;

namespace ABC.RepositoryManager.Application.Features.Repositories.DTOs
{
    public record OwnerGitHubResponse
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }
    }
}