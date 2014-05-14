using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class MatchPredictionConfiguration : EntityTypeConfiguration<MatchPrediction>
    {

        public MatchPredictionConfiguration()
        {
            Property(mp => mp.MatchPredictionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(mp => mp.User)
                .WithMany(u => u.MatchPredictions)
                .HasForeignKey(mp => mp.UserId)
                .WillCascadeOnDelete(true);
            Property(m => m.UserId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UN_MATCHPREDICTION_USER_MATCH", 1)));

            HasRequired(mp => mp.Match)
                .WithMany()
                .HasForeignKey(mp=>mp.MatchId)
                .WillCascadeOnDelete(false);
            Property(m => m.MatchId)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("UN_MATCHPREDICTION_USER_MATCH", 2) { IsUnique = true }));
        }

    }
}