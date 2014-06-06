using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class SystemParametersConfiguration : EntityTypeConfiguration<SystemParameters>
    {
        public SystemParametersConfiguration()
        {
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

    }
}