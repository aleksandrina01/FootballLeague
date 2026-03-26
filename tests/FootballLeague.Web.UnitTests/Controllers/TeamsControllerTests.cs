using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;

namespace FootballLeague.Web.UnitTests.Controllers
{
    public class TeamsControllerTests
    {
        private readonly Mock<ITeamService> _teamServiceMock;
        private readonly TeamsController _teamsController;
        public TeamsControllerTests()
        {
            _teamServiceMock = new Mock<ITeamService>();
            _teamsController = new TeamsController(_teamServiceMock.Object);
        }

        [Fact]
        public async Task GetAllTeams_ReturnsOk()
        {
            // Arrange
            var dtos = new List<TeamResponseDto>
            {
                new TeamResponseDto { Name = "CSKA", Country = "Bulgaria", City = "Sofia", Players = 20 }
            };

            _teamServiceMock.Setup(s => s.GetAllTeamsAsync()).ReturnsAsync(dtos);

            // Act
            var actionResult = await _teamsController.GetAllTeams();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returned = Assert.IsType<IEnumerable<TeamResponseDto>>(okResult.Value, exactMatch: false);
            Assert.Equal(dtos.Count, returned.Count());
        }

        [Fact]
        public async Task GetTeamByName_ReturnsOk()
        {
            // Arrange
            var expectedTeam = new TeamResponseDto { Name = "CSKA", Country = "Bulgaria", City = "Sofia", Players = 20 };

            _teamServiceMock.Setup(s => s.GetTeamByNameAsync(expectedTeam.Name)).ReturnsAsync(expectedTeam);

            // Act
            var actionResult = await _teamsController.GetTeamByName(expectedTeam.Name);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returned = Assert.IsType<TeamResponseDto>(okResult.Value, exactMatch: false);
            Assert.NotNull(returned);
        }

        [Fact]
        public async Task GetTeamByName_ReturnsNotFound()
        {
            // Arrange
            var name = "Liverpool";

            _teamServiceMock.Setup(s => s.GetTeamByNameAsync(name))
                .ThrowsAsync(new NotFoundException($"Team with name '{name}' not found."));

            // Act and Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _teamsController.GetTeamByName(name));
            Assert.Equal($"Team with name '{name}' not found.", exception.Message);
        }

        [Fact]
        public async Task CreateTeam_ReturnsCreated()
        {
            // Arrange
            var request = new TeamRequestDto { Name = "CSKA", Country = "Bulgaria", City = "Sofia", Players = 20 };
            var created = new TeamResponseDto { Id = Guid.NewGuid(), Name = request.Name!, Country = request.Country, City = request.City, Players = request.Players ?? 0 };

            _teamServiceMock.Setup(s => s.AddTeamAsync(request)).ReturnsAsync(created.Id);
            _teamServiceMock.Setup(s => s.GetTeamByIdAsync(created.Id)).ReturnsAsync(created);

            // Act
            var actionResult = await _teamsController.CreateTeam(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            var returned = Assert.IsType<Guid>(createdResult.Value);
            Assert.Equal(created.Id, returned);
        }

        [Fact]
        public async Task CreateTeam_ReturnsConflictException()
        {
            // Arrange
            var createdTeam = new TeamRequestDto { Name = "CSKA", Country = "Bulgaria", City = "Sofia", Players = 20 };

            _teamServiceMock.Setup(s => s.AddTeamAsync(createdTeam))
                .ThrowsAsync(new ConflictException($"Team with name '{createdTeam.Name}' already exists."));

            // Act and Assert
            var exception = await Assert.ThrowsAsync<ConflictException>(() => _teamsController.CreateTeam(createdTeam));
            Assert.Equal($"Team with name '{createdTeam.Name}' already exists.", exception.Message);
        }

        [Fact]
        public async Task DeleteTeam_ReturnsOk()
        {
            // Arrange
            var requestTeam = new TeamRequestDto { Name = "CSKA", Country = "Bulgaria", City = "Sofia", Players = 20 };
            var created = new TeamResponseDto { Id = Guid.NewGuid(), Name = requestTeam.Name!, Country = requestTeam.Country, 
                City = requestTeam.City, Players = requestTeam.Players ?? 0 };

            _teamServiceMock.Setup(s => s.AddTeamAsync(requestTeam)).ReturnsAsync(created.Id);
            _teamServiceMock.Setup(s => s.GetTeamByIdAsync(created.Id)).ReturnsAsync(created);

            // Act
            var result = await _teamsController.DeleteTeam(created.Id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteTeam_ReturnsNotFound()
        {
            // Arrange
            var createdTeam = new TeamRequestDto { Name = "CSKA", Country = "Bulgaria", City = "Sofia", Players = 20 };
            _teamServiceMock.Setup(s => s.AddTeamAsync(createdTeam));

            var id = Guid.NewGuid();
            _teamServiceMock.Setup(s => s.DeleteTeamAsync(id))
                .ThrowsAsync(new NotFoundException($"Team with id '{id}' not found."));

            // Act and Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _teamsController.DeleteTeam(id));
            Assert.Equal($"Team with id '{id}' not found.", exception.Message);
        }

        [Fact]
        public async Task GetRanking_ReturnsOrderedRanking()
        {
            // Arrange
            var teams = new List<TeamResponseDto>
            {
                new TeamResponseDto { Name = "TeamA", Points = 10 },
                new TeamResponseDto { Name = "TeamB", Points = 5 },
                new TeamResponseDto { Name = "TeamC", Points = 15 }
            };

            _teamServiceMock.Setup(s => s.GetAllTeamsAsync()).ReturnsAsync(teams);

            // Act
            var actionResult = await _teamsController.GetRanking();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var returned = Assert.IsType<IEnumerable<object>>(okResult.Value, exactMatch: false);

            var projected = returned.Select(x => new
            {
                Name = x.GetType().GetProperty("Name")!.GetValue(x) as string,
                Points = (int)x.GetType().GetProperty("Points")!.GetValue(x)!
            }).ToList();

            Assert.Equal(3, projected.Count);
            Assert.Equal(new[] { "TeamC", "TeamA", "TeamB" }, projected.Select(p => p.Name));
            Assert.Equal(new[] { 15, 10, 5 }, projected.Select(p => p.Points));
        }
    }
}
