using System;
using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class FootballMatchConfiguration : EntityTypeConfiguration<Match>
    {
        public FootballMatchConfiguration()
        {
            HasKey(m => new {m.HomeTeamId, m.AwayTeamId});
            HasRequired(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .WillCascadeOnDelete(false);
            HasRequired(m => m.AwayTeam)
                .WithMany(t=>t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .WillCascadeOnDelete(false);
        }

    }
}