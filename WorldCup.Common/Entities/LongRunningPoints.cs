using System;

namespace WorldCup.Common.Entities
{
    /// <summary>
    /// Contains the points for long running predictions for each user 
    /// </summary>
    public class LongRunningPoints
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int SecondStagePoints { get; set; }
        public int QuarterFinalPoints { get; set; }
        public int SemiFinalPoints { get; set; }
        public int SmallFinalPoints { get; set; }
        public int FinalPoints { get; set; }
        public int WinnerPoints { get; set; }

    }
}