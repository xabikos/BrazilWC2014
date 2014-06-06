using System;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class WinnerStatisticsConfiguration : EntityTypeConfiguration<WinnerStatistics>
    {
        public WinnerStatisticsConfiguration()
        {
            ToTable("WinnerStatistics");
            HasKey(t => t.Code);
        }
    
    }
}