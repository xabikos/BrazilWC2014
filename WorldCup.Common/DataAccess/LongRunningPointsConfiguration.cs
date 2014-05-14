using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class LongRunningPointsConfiguration : EntityTypeConfiguration<LongRunningPoints>
    {
        public LongRunningPointsConfiguration()
        {
            HasKey(lrp => lrp.UserId);
            Property(lrp => lrp.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(lrp => lrp.User).WithRequiredDependent(u => u.LongRunningPoints).WillCascadeOnDelete(true);
        }

    }
}