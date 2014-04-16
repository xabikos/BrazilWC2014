using System;
using System.ComponentModel.DataAnnotations;

namespace WorldCup.Common.Entities
{
    public class MatchPrediction
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual Match Match { get; set; }
        public int MatchId { get; set; }

        [Display(Name = "Final Result")]
        public MatchResult Result { get; set; }

        public int HomeTeamHalfTimeGoals { get; set; }

        public int AwayTeamHalfTimeGoals { get; set; }

        public int HomeTeamFullTimeGoals { get; set; }

        public int AwayTeamFullTimeGoals { get; set; }

        public int YellowCards { get; set; }

        public int RedCards { get; set; }

    }
}