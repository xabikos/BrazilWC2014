using System;
using System.ComponentModel.DataAnnotations;

namespace WorldCup.Common.Entities
{
    public class LongRunningPrediction
    {
        public LongRunningPrediction()
        {
            SecondStageTeamsIds = new string[16];
            QuarterFinalTeamsIds = new string[8];
            SemiFinalTeamsIds = new string[4];
            SmallFinalTeamsIds = new string[2];
            FinalTeamsIds = new string[2];
        }

        /// <summary>
        /// The id of the user the prediction belongs to
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The user the prediction belongs to
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Contains teams Ids as comma separated values for user's prediction 
        /// about the teams that will advanced to the round of 16
        /// </summary>
        public string SecondStageTeams
        {
            get { return string.Join(",", SecondStageTeamsIds); }
            set { value.Split(',').CopyTo(SecondStageTeamsIds, 0); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the round of 16
        /// </summary>
        [Display(Name = "Select the teams that will advanced to the round of 16")]
        [MaxLength(16, ErrorMessage = "You are allowed to select at most 16 teams")]
        public string[] SecondStageTeamsIds { get; set; }

        /// <summary>
        /// Contains teams Ids as comma separated values for user's prediction 
        /// about the teams that will advanced to the quarter final
        /// </summary>
        public string QuarterFinalTeams
        {
            get { return string.Join(",", QuarterFinalTeamsIds); }
            set { value.Split(',').CopyTo(QuarterFinalTeamsIds,0); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the quarter final
        /// </summary>
        [Display(Name = "Select the teams that will advanced to the Quarter finals")]
        [MaxLength(8, ErrorMessage = "You are allowed to select at most 8 teams")]
        public string[] QuarterFinalTeamsIds { get; set; }

        /// <summary>
        /// Contains teams Ids as comma separated values for user's prediction 
        /// about the teams that will advanced to semi final
        /// </summary>
        public string SemiFinalTeams
        {
            get { return string.Join(",", SemiFinalTeamsIds); }
            set { value.Split(',').CopyTo(SemiFinalTeamsIds, 0); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the semi final
        /// </summary>
        [Display(Name = "Select the teams that will advanced to the Semi finals")]
        [MaxLength(4, ErrorMessage = "You are allowed to select at most 4 teams")]
        public string[] SemiFinalTeamsIds { get; set; }

        /// <summary>
        /// Contains teams Ids as comma separated values for user's prediction 
        /// about the teams that will advanced to the small final (3rd place match)
        /// </summary>
        public string SmallFinalTeams
        {
            get { return string.Join(",", SmallFinalTeamsIds); }
            set { value.Split(',').CopyTo(SmallFinalTeamsIds, 0); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the small final (3rd place match)
        /// </summary>
        [Display(Name = "Select the teams that will advanced to the match for the third place")]
        [MaxLength(2, ErrorMessage = "You are allowed to select at most 2 teams")]
        public string[] SmallFinalTeamsIds { get; set; }

        /// <summary>
        /// Contains teams Ids as comma separated values for user's prediction 
        /// about the teams that will advanced to the final
        /// </summary>
        public string FinalTeams
        {
            get { return string.Join(",", FinalTeamsIds); }
            set { value.Split(',').CopyTo(FinalTeamsIds, 0); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the final
        /// </summary>
        [Display(Name = "Select the teams that will advanced to the Final")]
        [MaxLength(2, ErrorMessage = "You are allowed to select at most 2 teams")]
        public string[] FinalTeamsIds { get; set; }

        /// <summary>
        /// The id of the team that will win the competition
        /// </summary>
        [Display(Name = "Select the teams that will win the competition")]
        public string WinnerTeamId { get; set; }
        
        /// <summary>
        /// The team that will win the competition
        /// </summary>
        public virtual Team WinnerTeam { get; set; }

    }
}