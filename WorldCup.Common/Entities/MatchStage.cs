using System;
using System.ComponentModel.DataAnnotations;

namespace WorldCup.Common.Entities
{
    /// <summary>
    /// Describes the stage that each match belongs to
    /// </summary>
    public enum MatchStage
    {
        [Display(Name = "Group A")] GroupA,
        [Display(Name = "Group B")] GroupB,
        [Display(Name = "Group C")] GroupC,
        [Display(Name = "Group D")] GroupD,
        [Display(Name = "Group E")] GroupE,
        [Display(Name = "Group F")] GroupF,
        [Display(Name = "Group G")] GroupG,
        [Display(Name = "Group H")] GroupH,
        [Display(Name = "Round of 16")] Round16,
        [Display(Name = "Quarter-final")] QuarterFinal,
        [Display(Name = "Semi-final")] SemiFinal,
        [Display(Name = "Small Final")] ThirdPlace,
        [Display(Name = "Final")] Final

    }
}