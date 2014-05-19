using System;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class ParametersConfiguration : EntityTypeConfiguration<Parameter>
    {
        public ParametersConfiguration()
        {
            HasKey(p => p.Name);
        }

    }
}