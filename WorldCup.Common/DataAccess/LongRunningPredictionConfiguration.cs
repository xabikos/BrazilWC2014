using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class LongRunningPredictionConfiguration : EntityTypeConfiguration<LongRunningPrediction>
    {
        public LongRunningPredictionConfiguration()
        {
            HasKey(lrp => lrp.UserId);
            Property(lrp => lrp.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(lrp => lrp.User).WithRequiredDependent(u => u.LongRunningPrediction).WillCascadeOnDelete(true);

            HasOptional(lrp => lrp.WinnerTeam)
                .WithMany()
                .HasForeignKey(lrp => lrp.WinnerTeamId)
                .WillCascadeOnDelete(false);
            
            Ignore(lrp => lrp.SecondStageTeamsIds);
        }

    }
}