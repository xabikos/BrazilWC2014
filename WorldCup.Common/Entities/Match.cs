using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WorldCup.Common.Entities
{
    public class Match
    {
        public int Id { get; set; }

        public MatchState State { get; set; }

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

        [Range(0, 15, ErrorMessage = "The value should be between 0 and 15")]
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