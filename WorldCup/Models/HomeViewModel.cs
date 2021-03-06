﻿using System;
using System.Collections.Generic;
using WorldCup.Common.Entities;

namespace WorldCup.Models
{
    public class HomeViewModel
    {
        public string Logo { get; set; }
        public string LogoText { get; set; } 

        public string IntroductionText { get; set; } 

        /// <summary>
        /// List that contains the latest sign in user results
        /// </summary>
        public IList<UserMatchModel> UserLatestResults { get; set; }

        /// <summary>
        /// List that contains the five upcoming matched
        /// </summary>
        public IList<Match> UpcomingMatches { get; set; }

        /// <summary>
        /// List that contains the two nearest in the future predictions
        /// </summary>
        public IList<MatchPrediction> UserPredictionMatches { get; set; }
    }

    public class UserMatchModel
    {
        public int MatchId { get; set; }
        public DateTime Date { get; set; }
        public string Match { get; set; }
        public int Points { get; set; }
    }

}