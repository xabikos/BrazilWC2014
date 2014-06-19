using System;
using System.Collections.Generic;

namespace WorldCup.Models.Predictions
{
    /// <summary>
    /// View model used to show the info of a match once it has started
    /// </summary>
    public class PredictionInfoModel
    {
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }

        public UserPredictionInfo UserPredictionInfo { get; set; }

        public bool FinalResultsUpdated { get; set; }
        public string HalfTimeScoreResult { get; set; }
        public string FullTimeScoreResult { get; set; }
        public string WinnerResult { get; set; }
        public int YellowCardsResult { get; set; }
        public int RedCardsResult { get; set; }

        public int HomeTeamWinnerCount { get; set; }
        public int DrawCount { get; set; }
        public int AwayTeamWinnerCount { get; set; }

        public IList<UserPredictionInfo> UsersPredictions { get; set; } 
    }

    public class UserPredictionInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string HalfTimeScore { get; set; }
        public string FullTimeScore { get; set; }
        public string Winner { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int MatchPoints { get; set; }
    }
}