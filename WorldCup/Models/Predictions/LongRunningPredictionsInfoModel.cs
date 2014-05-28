using System;

namespace WorldCup.Models.Predictions
{
    /// <summary>
    /// View model used to show the info of long running predictions once the tournament has started
    /// </summary>
    public class LongRunningPredictionsInfoModel
    {
        public string Round16Teams { get; set; }
        public int Round16TeamsPoints { get; set; }
        public string QuarterFinalTeams { get; set; }
        public int QuarterFinalPoints { get; set; }
        public string SemiFinalTeams { get; set; }
        public int SemiFinalPoints { get; set; }
        public string SmallFinalTeams { get; set; }
        public int SmallFinalPoints { get; set; }
        public string FinalTeams { get; set; }
        public int FinalPoints { get; set; }
        public string WinnerTeam { get; set; }
        public int WinnerPoints { get; set; }
    }
}