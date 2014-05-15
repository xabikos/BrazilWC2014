using System;
using System.Collections.Generic;
using WorldCup.Common.Entities;

namespace WorldCup.Models
{
    public class HomeViewModel
    {
        /// <summary>
        /// List that contains the five upcoming matched
        /// </summary>
        public IList<Match> UpcomingMatches { get; set; }

        public IList<UserMatchModel> UserLatestResults { get; set; }

    }

    public class UserMatchModel
    {
        public DateTime Date { get; set; }
        public string Match { get; set; }
        public int Points { get; set; }
    }
}