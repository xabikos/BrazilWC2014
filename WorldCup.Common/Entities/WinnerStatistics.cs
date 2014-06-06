using System;

namespace WorldCup.Common.Entities
{
    /// <summary>
    /// Holds the number of times the team has selected to win the tournament
    /// </summary>
    public class WinnerStatistics
    {
        public string Code { get; set; }
        public int Count { get; set; }
    }
}