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
                new {Id = 0, match.HomeTeam.Name},
                new {Id = 2, match.AwayTeam.Name}
            };

            // Add the draw item only for group stage matches
            // In all other matches a winner should exists
            if(match.IsGroupStage())
                items.Insert(1, (new {Id = 1, Name = "Draw"}));

            return new SelectList(items, "Id", "Name", matchResult);
        }

        public static string WinnerTeamName(this MatchResult matchResult, Match match)
        {
            if (matchResult == MatchResult.Home)
                return match.HomeTeam.Name;

            return matchResult == MatchResult.Away ? match.AwayTeam.Name : "Draw";
        }

    }
}