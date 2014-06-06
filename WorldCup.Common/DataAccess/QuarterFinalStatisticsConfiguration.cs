using System;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class QuarterFinalStatisticsConfiguration : EntityTypeConfiguration<QuarterFinalStatistics>
    {
        public QuarterFinalStatisticsConfiguration()
        {
            ToTable("QuarterFinalStatistics");
            HasKey(t => t.Code);
        }

    }
}