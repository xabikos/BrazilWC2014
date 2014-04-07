using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class FootballMatchConfiguration : EntityTypeConfiguration<Match>
    {
        public FootballMatchConfiguration()
        {
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .WillCascadeOnDelete(false);
            Property(m => m.HomeTeamId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UN_HOMETEAM_AWAYTEAM", 1)));
            
            HasRequired(m => m.AwayTeam)
                .WithMany(t=>t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .WillCascadeOnDelete(false);
            Property(m => m.AwayTeamId)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("UN_HOMETEAM_AWAYTEAM", 2) {IsUnique = true}));
        }

    }
}