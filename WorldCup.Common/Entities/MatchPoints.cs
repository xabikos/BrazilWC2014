using System;

namespace WorldCup.Common.Entities
{
    public class MatchPoints
    {
        public int MatchPointsId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual Match Match { get; set; }
        public int MatchId { get; set; }

        public int Points { get; set; }

    }
}