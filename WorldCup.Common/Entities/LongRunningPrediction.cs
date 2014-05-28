using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        [Display(Name = "Select the teams that will advance to the round of 16")]
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
        [Display(Name = "Select the teams that will advance to the quarter finals")]
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
        [Display(Name = "Select the teams that will advance to the semi finals")]
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
        [Display(Name = "Select the teams that will advance to the match for the third place")]
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
        [Display(Name = "Select the teams that will play the final")]
        [MaxLength(2, ErrorMessage = "You are allowed to select at most 2 teams")]
        public string[] FinalTeamsIds { get; set; }

        /// <summary>
        /// The id of the team that will win the competition
        /// </summary>
        [Display(Name = "Select the team that will win the world cup")]
        public string WinnerTeamId { get; set; }
        
        /// <summary>
        /// The team that will win the competition
        /// </summary>
        public virtual Team WinnerTeam { get; set; }

        /// <summary>
        /// Returns the names for the selected teams that will advance to the second stage
        /// </summary>
        public string GetSecondStageSelectedTeamsNames
        {
            get
            {
                return string.Join(", ",
                    SecondStageTeamsIds.Where(s => !string.IsNullOrEmpty(s)).Select(GetTeamName));
            }
        }

        /// <summary>
        /// Returns the names for the selected teams that will advance to the Quarter final
        /// </summary>
        public string GetQaurterFinalSelectedTeamsNames
        {
            get
            {
                return string.Join(", ",
                    QuarterFinalTeamsIds.Where(s => !string.IsNullOrEmpty(s)).Select(GetTeamName));
            }
        }

        /// <summary>
        /// Returns the names for the selected teams that will advance to the Semi final
        /// </summary>
        public string GetSemiFinalSelectedTeamsNames
        {
            get
            {
                return string.Join(", ",
                    SemiFinalTeamsIds.Where(s => !string.IsNullOrEmpty(s)).Select(GetTeamName));
            }
        }

        /// <summary>
        /// Returns the names for the selected teams that will advance to the small final
        /// </summary>
        public string GetSmallFinalSelectedTeamsNames
        {
            get
            {
                return string.Join(", ",
                    SmallFinalTeamsIds.Where(s => !string.IsNullOrEmpty(s)).Select(GetTeamName));
            }
        }

        /// <summary>
        /// Returns the names for the selected teams that will advance to the final
        /// </summary>
        public string GetFinalSelectedTeamsNames
        {
            get
            {
                return string.Join(", ",
                    FinalTeamsIds.Where(s => !string.IsNullOrEmpty(s)).Select(GetTeamName));
            }
        }

        private static string GetTeamName(string teamCode)
        {
            switch (teamCode.ToLowerInvariant())
            {
                case "alg":
                    return "Algeria";
                case "arg":
                    return "Argentina";
                case "aus":
                    return "Australia";
                case "bel":
                    return "Belgium";
                case "bih":
                    return "Bosnia-Herzegovina";
                case "bra":
                    return "Brazil";
                case "chi":
                    return "Chile";
                case "civ":
                    return "Cote d'Ivore";
                case "cmr":
                    return "Cameroon";
                case "col":
                    return "Colombia";
                case "crc":
                    return "Costa Rica";
                case "cro":
                    return "Croatia";
                case "ecu":
                    return "Ecuador";
                case "eng":
                    return "England";
                case "esp":
                    return "Spain";
                case "fra":
                    return "France";
                case "ger":
                    return "Germany";
                case "gha":
                    return "Ghana";
                case "gre":
                    return "Greece";
                case "hon":
                    return "Honduras";
                case "irn":
                    return "Iran";
                case "ita":
                    return "Italy";
                case "jpn":
                    return "Japan";
                case "kor":
                    return "South Korea";
                case "mex":
                    return "Mexico";
                case "ned":
                    return "Netherlands";
                case "nga":
                    return "Nigeria";
                case "por":
                    return "Portugal";
                case "rus":
                    return "Russia";
                case "sui":
                    return "Switzerland";
                case "uru":
                    return "Uruguay";
                case "usa":
                    return "United States";
                default:
                    return "No valid code";
            }
        }

    }
}