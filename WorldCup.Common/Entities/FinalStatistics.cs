using System;

namespace WorldCup.Common.Entities
{
    /// <summary>
    /// Holds the number of times the team has selected to advanced to the final
    /// </summary>
    public class FinalStatistics
    {
        public string Code { get; set; }
        public int Count { get; set; }
    }
}