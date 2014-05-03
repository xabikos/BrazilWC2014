using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WorldCup.Common.Entities
{
    public class LongRunningPrediction
    {
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
            set { SecondStageTeamsIds = new Collection<string>(value.Split(',')); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the round of 16
        /// </summary>
        public ICollection<string> SecondStageTeamsIds { get; set; }

        /// <summary>
        /// Contains teams Ids as comma separated values for user's prediction 
        /// about the teams that will advanced to the quarter final
        /// </summary>
        public string QuarterFinalTeams
        {
            get { return string.Join(",", QuarterFinalTeamsIds); }
            set { QuarterFinalTeamsIds = new Collection<string>(value.Split(',')); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the quarter final
        /// </summary>
        public ICollection<string> QuarterFinalTeamsIds { get; set; }

        /// <summary>
        /// Contains teams Ids as comma separated values for user's prediction 
        /// about the teams that will advanced to semi final
        /// </summary>
        public string SemiFinalTeams
        {
            get { return string.Join(",", SemiFinalTeamsIds); }
            set { SecondStageTeamsIds = new Collection<string>(value.Split(',')); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the semi final
        /// </summary>
        public ICollection<string> SemiFinalTeamsIds { get; set; }

        /// <summary>
        /// Contains teams Ids as comma separated values for user's prediction 
        /// about the teams that will advanced to the small final (3rd place match)
        /// </summary>
        public string SmallFinalTeams
        {
            get { return string.Join(",", SmallFinalTeamsIds); }
            set { SmallFinalTeamsIds = new Collection<string>(value.Split(',')); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the small final (3rd place match)
        /// </summary>
        public ICollection<string> SmallFinalTeamsIds { get; set; }

        /// <summary>
        /// Contains teams Ids as comma separated values for user's prediction 
        /// about the teams that will advanced to the final
        /// </summary>
        public string FinalTeams
        {
            get { return string.Join(",", FinalTeamsIds); }
            set { FinalTeamsIds = new Collection<string>(value.Split(',')); }
        }

        /// <summary>
        /// Contains the Ids for user's prediction about the teams that will advanced to the final
        /// </summary>
        public ICollection<string> FinalTeamsIds { get; set; }

        /// <summary>
        /// The id of the team that will win the competition
        /// </summary>
        public string WinnerTeamId { get; set; }
        
        /// <summary>
        /// The team that will win the competition
        /// </summary>
        public virtual Team WinnerTeam { get; set; }

    }
}