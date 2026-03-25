using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Match
    {
        public Guid Id { get; set; }

        public Guid FirstTeamId { get; set; }

        //public Team? FirstTeam { get; set; }

        public Guid SecondTeamId { get; set; }

        //public Team? SecondTeam { get; set; }

        public int? FirstTeamScore { get; set; }

        public int? SecondTeamScore { get; set; }

        public DateTime? PlayedAt { get; set; }

        public bool IsPlayed => FirstTeamScore.HasValue && SecondTeamScore.HasValue;
    }
}
