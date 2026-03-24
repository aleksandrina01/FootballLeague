using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class MatchSummaryDto
    {
        public Guid Id { get; set; }

        public DateTime? Date { get; set; }

        public string OpponentName { get; set; }

        public bool IsHome { get; set; }

        public int? GoalsFor { get; set; }

        public int? GoalsAgainst { get; set; }

        public string? Result { get; set; }
    }

}
