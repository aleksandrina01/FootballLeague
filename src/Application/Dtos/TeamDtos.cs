using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class TeamRequestDto
    {
        [Required]
        public string? Name { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public int? Players { get; set; }
    }

    public class TeamResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public int Players { get; set; }

        public int Points { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public static TeamResponseDto ToDto(Team team)
        {
            return new TeamResponseDto
            {
                Id = team.Id,
                Name = team.Name,
                Country = team.Country,
                City = team.City,
                Players = team.Players,
                Wins = team.Wins,
                Draws = team.Draws,
                Losses = team.Losses,
                Points = team.Points
            };
        }
    }
}