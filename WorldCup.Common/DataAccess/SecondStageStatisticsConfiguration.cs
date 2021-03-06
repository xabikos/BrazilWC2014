﻿using System;
using System.Data.Entity.ModelConfiguration;
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