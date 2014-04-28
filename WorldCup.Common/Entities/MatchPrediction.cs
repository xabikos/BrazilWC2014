using System;
using System.ComponentModel.DataAnnotations;

namespace WorldCup.Common.Entities
{
    public class MatchPrediction
    {
        public int MatchPredictionId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual Match Match { get; set; }
        public int MatchId { get; set; }

        [Display(Name = "Final Result")]
        public MatchResult Result { get; set; }

        [Range(0,15, ErrorMessage = "The value should be between 0 and 15")]
        public int HomeTeamHalfTimeGoals { get; set; }

        [Range(0, 15, ErrorMessage = "The value should be between 0 and 15")]
        public int AwayTeamHalfTimeGoals { get; set; }

        [Range(0, 15, ErrorMessage = "The value should be between 0 and 15")]
        public int HomeTeamFullTimeGoals { get; set; }

        [Range(0, 15, ErrorMessage = "The value should be between 0 and 15")]
        public int AwayTeamFullTimeGoals { get; set; }

        [Range(0, 40, ErrorMessage = "The value should be between 0 and 40")]
        public int YellowCards { get; set; }

        [Range(0, 25, ErrorMessage = "The value should be between 0 and 25")]
        public int RedCards { get; set; }

    }
}