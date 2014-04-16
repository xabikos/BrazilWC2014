using System;
using System.ComponentModel.DataAnnotations;

namespace WorldCup.Common.Entities
{
    /// <summary>
    /// Describes the stage that each match belongs to
    /// </summary>
    public enum MatchStage
    {
        [Display(Name="Group A")]
        GroupA,
        GroupB,
        GroupC,
        GroupD,
        GroupE,
        GroupF,
        GroupG,
        GroupH,
        Round16,
        QuarterFinal,
        SemiFinal,
        ThirdPlace,
        Final

    }
}