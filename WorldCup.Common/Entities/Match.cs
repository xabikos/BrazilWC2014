using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WorldCup.Common.Entities
{
    public class Match
    {
        public int Id { get; set; }

        public MatchStage Stage { get; set; }

        public virtual Team HomeTeam { get; set; }
        
        [JsonProperty(PropertyName = "team1_key")]
        [Required]
        [Display(Name = "Home Team")]
        public string HomeTeamId { get; set; }
        
        public virtual Team AwayTeam { get; set; }
        
        [JsonProperty(PropertyName = "team2_key")]
        [Required]
        [Display(Name = "Away Team")]
        public string AwayTeamId { get; set; }

        [JsonProperty(PropertyName = "play_at")]
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// The result in the half time
        /// </summary>
        public MatchResult HalfTimeResult
        {
            get
            {
                if (HomeTeamHalfTimeGoals > AwayTeamHalfTimeGoals)
                    return MatchResult.Home;
                if (HomeTeamHalfTimeGoals == AwayTeamHalfTimeGoals)
                    return MatchResult.Draw;
                return MatchResult.Away;
            }
        }

        /// <summary>
        /// The result in the full time
        /// </summary>
        public MatchResult FullTimeResult
        {
            get
            {
                if (HomeTeamFullTimeGoals > AwayTeamFullTimeGoals)
                    return MatchResult.Home;
                if (HomeTeamFullTimeGoals == AwayTeamFullTimeGoals)
                    return MatchResult.Draw;
                return MatchResult.Away;
            }
        }

        /// <summary>
        /// The final result of the match. CAn be different from full time result in case there is 
        /// extra time or penalties
        /// </summary>
        [Display(Name = "Final Result")]
        public MatchResult Result { get; set; }

        public int? HomeTeamHalfTimeGoals { get; set; }

        public int? AwayTeamHalfTimeGoals { get; set; }

        public int? HomeTeamFullTimeGoals { get; set; }

        public int? AwayTeamFullTimeGoals { get; set; }

        public int? YellowCards { get; set; }

        public int? RedCards { get; set; }

        public virtual ICollection<MatchPrediction> MatchPredictions { get; set; }
       
    }
}