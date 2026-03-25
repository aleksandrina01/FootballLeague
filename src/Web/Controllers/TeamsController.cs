using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TeamResponseDto>>> GetAllTeams()
        {
            return Ok(await _teamService.GetAllTeamsAsync());
        }

        [HttpGet("team/{name}")]
        public async Task<ActionResult<TeamResponseDto>> GetTeamByName(string name)
        {
            var team = await _teamService.GetTeamByNameAsync(name);

            return Ok(team);
        }

        [HttpPost("team")]
        public async Task<ActionResult> CreateTeam([FromBody] TeamRequestDto team)
        {
            await _teamService.AddTeamAsync(team);

            return Ok();
        }

        [HttpPut("team/{teamId:guid}")]
        public async Task<ActionResult> UpdateTeam(Guid teamId, [FromBody] TeamRequestDto team)
        {
            await _teamService.UpdateTeamAsync(teamId, team);

            return Ok();
        }

        [HttpDelete("team/{teamId:guid}")]
        public async Task<ActionResult> DeleteTeam(Guid teamId)
        {
            await _teamService.DeleteTeamAsync(teamId);

            return Ok();
        }

        [HttpGet("ranking")]
        public async Task<IActionResult> GetRanking()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            var ranking = teams
                .Select(t => new { t.Name, t.Points })
                .OrderByDescending(t => t.Points)
                .ToList();

            return Ok(ranking);
        }
    }
}
