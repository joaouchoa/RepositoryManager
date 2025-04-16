using MediatR;
using Microsoft.AspNetCore.Mvc;
using ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName;
using ABC.RepositoryManager.Application.Features.Repositories.Commands.CreateFavoriteRepo;
using ABC.RepositoryManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo;
using ABC.RepositoryManager.Application.Features.Repositories.Queries.GetFavoriteRepos;

namespace ABC.RepositoryManager.API.Controllers
{
    /// <summary>
    /// Controller responsible for searching and managing GitHub repositories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReposController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReposController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Searches for public GitHub repositories based on the provided name.
        /// </summary>
        /// <param name="query">Object containing the repository name, pagination, sorting, and items per page.</param>
        /// <returns>A list of public repositories matching the provided name, with pagination and sorting.</returns>
        [HttpGet("search", Name = "GetReposByName")]
        [ProducesResponseType(typeof(GetRepoByNameQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReposByNameAsync([FromQuery] GetRepoByNameQuery query)
        {
            var response = await _mediator.Send(query, HttpContext.RequestAborted);
            return response.ToActionResult();
        }


        /// <summary>
        /// Adds a repository to the list of locally saved favorites.
        /// </summary>
        /// <param name="repository">The repository information to be added to favorites.</param>
        /// <returns>The result of the favorite operation.</returns>
        [HttpPost("favorite", Name = "CreateFavoriteRepo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRepoAsync([FromBody] CreateFavoriteRepoCommand repository)
        {
            var response = await _mediator.Send(repository, HttpContext.RequestAborted);
            return response.ToActionResult();
        }

        /// <summary>
        /// Removes a repository from the list of favorites.
        /// </summary>
        /// <param name="repository">The repository information to be removed from favorites (ID only).</param>
        /// <returns>The result of the remove operation.</returns>
        [HttpDelete("remove-favorite", Name = "DeleteRepo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRepoAsync([FromBody] DeleteFavoriteRepoCommand repository)
        {
            var response = await _mediator.Send(repository, HttpContext.RequestAborted);
            return response.ToActionResult();
        }


        /// <summary>
        /// Lists all favorited repositories with support for pagination and sorting.
        /// </summary>
        /// <param name="query">Pagination and sorting parameters.</param>
        /// <returns>A paged list of favorited repositories.</returns>
        [HttpGet("favorite-repositories", Name = "GetfavoriteRepos")]
        [ProducesResponseType(typeof(GetFavoriteReposQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFavoritesRepos([FromQuery] GetFavoriteReposQuery query)
        {
            var response = await _mediator.Send(query, HttpContext.RequestAborted);
            return response.ToActionResult();
        }
    }
}
