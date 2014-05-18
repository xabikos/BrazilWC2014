using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class RaisedMoneyConfiguration : EntityTypeConfiguration<RaisedMoney>
    {
        public RaisedMoneyConfiguration()
        {
            ToTable("RaisedMoney");

            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(m => m.Date)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("UN_RAISEDMONEY_DATE") {IsUnique = true}));
        }

    }
}