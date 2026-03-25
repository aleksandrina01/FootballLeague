using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
	public class TeamRequestDto
	{
        public string Name { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public int? Players { get; set; }
    }
}
