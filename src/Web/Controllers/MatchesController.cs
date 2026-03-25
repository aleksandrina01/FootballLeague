using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet("matches")]
        public async Task<ActionResult<MatchResponseDto>> GetAllMatches()
        {
            var matches = await _matchService.GetAllMatchesAsync();
            return Ok(matches);
        }

        [HttpGet("match/{id:guid}")]
        public async Task<ActionResult<MatchResponseDto>> GetMatchById(Guid id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            return Ok(match);
        }

        [HttpPost("match")]
        public async Task<ActionResult> CreateMatch([FromBody]MatchRequestDto match)
        {
            await _matchService.CreateMatchAsync(match);
            return Ok();
        }

        [HttpDelete("match/{id:guid}")]
        public async Task<ActionResult> DeleteMatch(Guid id)
        {
            await _matchService.DeleteMatchAsync(id);
            return Ok();
        }
    }
}
