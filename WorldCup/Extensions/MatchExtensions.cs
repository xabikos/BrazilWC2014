using System;
using WorldCup.Common.Entities;

namespace WorldCup.Extensions
{
    /// <summary>
    /// Contains extension methods for Match
    /// </summary>
    public static class MatchExtensions
    {
        /// <summary>
        /// Indicates if a Group or next round match
        /// </summary>
        public static bool IsGroupStage(this Match match)
        {
            return match.Stage == MatchStage.GroupA ||
                   match.Stage == MatchStage.GroupB ||
                   match.Stage == MatchStage.GroupC ||
                   match.Stage == MatchStage.GroupD ||
                   match.Stage == MatchStage.GroupE ||
                   match.Stage == MatchStage.GroupF ||
                   match.Stage == MatchStage.GroupG ||
                   match.Stage == MatchStage.GroupH;
        }

    }
}