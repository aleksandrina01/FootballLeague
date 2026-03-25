using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly ITeamService _teamService;

        public TeamsController(ILogger<TeamsController> logger, ITeamService teamService)
        {
            _logger = logger;
            _teamService = teamService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetAllTeams()
        {
            return Ok(await _teamService.GetAllTeamsAsync());
        }

        [HttpGet("team/{name}")]
        public async Task<ActionResult<TeamDto>> GetTeamByName(string name)
        {
            var team = await _teamService.GetTeamByNameAsync(name);

            if (team is null) return NotFound();

            return Ok(team);
        }

        [HttpPost("team")]
        public async Task<ActionResult> CreateTeam([FromBody] Team team)
        {
            await _teamService.AddTeamAsync(team);

            return Ok();
        }

        [HttpPut("team")]
        public async Task<ActionResult> UpdateTeam([FromBody] Team team)
        {
            await _teamService.UpdateTeamAsync(team);

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
            return Ok(teams.OrderByDescending(t => t.Points));
        }
    }
}
