using System;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class SemiFinalStatisticsConfiguration : EntityTypeConfiguration<SemiFinalStatistics>
    {
        public SemiFinalStatisticsConfiguration()
        {
            ToTable("SemiFinalStatistics");
            HasKey(t => t.Code);
        }
    
    }
}