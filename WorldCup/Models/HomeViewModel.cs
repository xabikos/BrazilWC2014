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

    }
}