using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup.Common.Entities
{
    public class FootballPlayer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Old { get; set; }
        public FootballTeam Team { get; set; }

    }
}