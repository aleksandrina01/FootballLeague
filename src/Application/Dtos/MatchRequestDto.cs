using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos
{
	public class MatchRequestDto
	{
		public Guid FirstTeamId { get; set; }
		public int FirstTeamScore { get; set; }
		public Guid SecondTeamId { get; set; }
		public int SecondTeamScore { get; set; }
		[JsonIgnore]
		public DateTime PlayedAt { get; set; } = DateTime.UtcNow;
	}
}
