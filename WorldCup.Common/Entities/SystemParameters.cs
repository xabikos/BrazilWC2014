using System;
using System.ComponentModel.DataAnnotations;

namespace WorldCup.Common.Entities
{
    public class SystemParameters
    {
        public int Id { get; set; }

        [Range(0,100)]
        public int GroupHalfTimeScoreFactor { get; set; }
        [Range(0,100)]
        public int GroupFullTimeScoreFactor { get; set; }
        [Range(0,100)]
        public int GroupWinnerFactor { get; set; }
        [Range(0,100)]
        public int GroupYellowCardsNumberFactor { get; set; }
        [Range(0,100)]
        public int GroupRedCardsNumberFactor { get; set; }

        [Range(0,100)]
        public int FinalsHalfTimeScoreFactor { get; set; }
        [Range(0,100)]
        public int FinalsFullTimeScoreFactor { get; set; }
        [Range(0,100)]
        public int FinalsWinnerFactor { get; set; }
        [Range(0,100)]
        public int FinalsYellowCardsNumberFactor { get; set; }
        [Range(0,100)]
        public int FinalsRedCardsNumberFactor { get; set; }

        [Range(0,100)]
        public int Round16TeamsFactor { get; set; }
        [Range(0,100)]
        public int QuarterFinalTeamsFactor { get; set; }
        [Range(0,100)]
        public int SemiFinalTeamsFactor { get; set; }
        [Range(0,100)]
        public int SmallFinalTeamsFactor { get; set; }
        [Range(0,100)]
        public int FinalTeamsFactor { get; set; }

        [Range(0,100)]
        public int WinnerTeamFactor { get; set; }

    }
}