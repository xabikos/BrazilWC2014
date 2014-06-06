using System;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class SmallFinalStatisticsConfiguration : EntityTypeConfiguration<SmallFinalStatistics>
    {
        public SmallFinalStatisticsConfiguration()
        {
            ToTable("SmallFinalStatistics");
            HasKey(t => t.Code);
        }
    
    }
}