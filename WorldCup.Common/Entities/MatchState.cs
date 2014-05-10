using System;

namespace WorldCup.Common.Entities
{

    public enum MatchState
    {
        /// <summary>
        /// When a match is created
        /// </summary>
        Created,
        /// <summary>
        /// When a match is available for predictions
        /// </summary>
        VisibleForPredictions,
        /// <summary>
        /// When a match has finished and all the details have filled so the calculations can be done
        /// </summary>
        Finalized
    }
}