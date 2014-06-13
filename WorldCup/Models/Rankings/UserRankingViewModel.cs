using System;

namespace WorldCup.Models.Rankings
{
    public class UserRankingViewModel
    {
        public int Postion { get; set; }
        public string Name { get; set; }
        public int MatchPoints { get; set; }
        public int LongRunningPoints { get; set; }
        public int TotalPoints { get { return MatchPoints + LongRunningPoints; } }
        public string UserName { get; set; }
    }
}