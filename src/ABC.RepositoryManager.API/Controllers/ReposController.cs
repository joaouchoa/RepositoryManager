using MediatR;
using Microsoft.AspNetCore.Mvc;
using ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName;
using ABC.RepositoryManager.Application.Features.Repositories.Commands.CreateFavoriteRepo;
using ABC.RepositoryManager.Application.Features.Repositories.Commands.DeleteFavoriteRepo;
using ABC.RepositoryManager.Application.Features.Repositories.Queries.GetFavoriteRepos;

namespace ABC.RepositoryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReposController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReposController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search", Name = "GetReposByName")]
        public async Task<IActionResult> GetReposByNameAsync([FromQuery] GetRepoByNameQuery query)
        {
            var response = await _mediator.Send(query, HttpContext.RequestAborted);
            return response.ToActionResult(); 
        }

        [HttpPost("favorite", Name = "CreateFavoriteRepo")]
        public async Task<IActionResult> CreateRepoAsync([FromBody] CreateFavoriteRepoCommand repository)
        {
            var response = await _mediator.Send(repository, HttpContext.RequestAborted);
            return response.ToActionResult();
        }

        [HttpDelete("remove-favorite", Name = "DeleteRepo")]
        public async Task<IActionResult> DeleteRepoAsync([FromBody] DeleteFavoriteRepoCommand repository)
        {
            var response = await _mediator.Send(repository, HttpContext.RequestAborted);
            return response.ToActionResult();
        }

        [HttpGet("favorite-repositories", Name = "GetfavoriteRepos")]
        public async Task<IActionResult> GetFavoritesRepos([FromQuery] GetFavoriteReposQuery query)
        {
            var response = await _mediator.Send(query, HttpContext.RequestAborted);
            return response.ToActionResult();
        }
    }
}
