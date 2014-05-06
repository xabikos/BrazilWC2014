using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WorldCup.Common.Entities;

namespace WorldCup.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="MatchResult"/>
    /// </summary>
    public static class MatchResultExtensions
    {
        public static SelectList ToSelectList(this MatchResult matchResult, Match match)
        {
            var items = new List<object>
            {
                new {Id = 0, Name = match.HomeTeam.Name},
                new {Id = 1, Name = "Draw"},
                new {Id = 2, Name = match.AwayTeam.Name}
            };

            return new SelectList(items, "Id", "Name", matchResult);
        }

    }
}