using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _service;

        public MatchesController(IMatchService service)
        {
            _service = service;
        }

        //[HttpGet("match/{id:guid}")]
        //public async Task<ActionResult<MatchDto>> GetMatch(Guid id)
        //{
        //    await _service.GetMatchAsync(id);
        //    return Ok();
        //}

        [HttpPost("match")]
        public async Task<ActionResult> Create([FromBody]MatchRequestDto match)
        {
            await _service.AddMatchAsync(match);
            return Ok();
        }
    }
}
