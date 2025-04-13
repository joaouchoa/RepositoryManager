using MediatR;
using Microsoft.AspNetCore.Mvc;
using ABC.RepositoryManager.Application.Features.Repositories.Queries.GetRepoByName;

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

        [HttpGet]
        public async Task<IActionResult> GetReposByNameAsync([FromQuery] GetRepoByNameQuery query)
        {
            var response = await _mediator.Send(query, HttpContext.RequestAborted);
            return response.ToActionResult(); 
        }
    }
}
