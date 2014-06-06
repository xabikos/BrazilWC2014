using System;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class FinalStatisticsConfiguration : EntityTypeConfiguration<FinalStatistics>
    {
        public FinalStatisticsConfiguration()
        {
            ToTable("FinalStatistics");
            HasKey(t => t.Code);
        }
    
    }
}