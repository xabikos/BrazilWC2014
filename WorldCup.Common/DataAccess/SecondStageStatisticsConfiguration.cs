using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class SecondStageStatisticsConfiguration : EntityTypeConfiguration<SecondStageStatistics>
    {
        public SecondStageStatisticsConfiguration()
        {
            ToTable("SecondStageStatistics");
            HasKey(t => t.Code);
        }

    }
}