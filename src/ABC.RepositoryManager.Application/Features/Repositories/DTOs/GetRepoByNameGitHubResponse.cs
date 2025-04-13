using System.Text.Json.Serialization;

namespace ABC.RepositoryManager.Application.Features.Repositories.DTOs
{
    public record GetRepoByNameGitHubResponse(
        [property: JsonPropertyName("total_count")] int TotalCount,
        [property: JsonPropertyName("items")] List<RepositoryGitHubResponse> Repositories,
        [property: JsonIgnore] bool HasMorePages,
        [property: JsonIgnore] int TotalPages
    );
}
