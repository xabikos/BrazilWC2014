using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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