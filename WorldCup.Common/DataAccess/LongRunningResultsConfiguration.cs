using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class LongRunningResultsConfiguration : EntityTypeConfiguration<LongRunningResults>
    {

        public LongRunningResultsConfiguration()
        {
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(lrp => lrp.WinnerTeam)
                .WithMany()
                .HasForeignKey(lrp => lrp.WinnerTeamId)
                .WillCascadeOnDelete(false);

            Ignore(lrp => lrp.SecondStageTeamsIds);
            Ignore(lrp => lrp.QuarterFinalTeamsIds);
            Ignore(lrp => lrp.SemiFinalTeamsIds);
            Ignore(lrp => lrp.SmallFinalTeamsIds);
            Ignore(lrp => lrp.FinalTeamsIds);
        }

    }
}