using System.Data.Entity.ModelConfiguration;
using WorldCup.Common.Entities;

namespace WorldCup.Common.DataAccess
{
    public class FootballTeamConfiguration : EntityTypeConfiguration<FootballTeam>
    {
        public FootballTeamConfiguration()
        {
            HasKey(ft => ft.Id);
            Property(ft => ft.Name).IsRequired().HasMaxLength(100);
            Property(ft => ft.Code).IsRequired().HasMaxLength(3);
        }

    }
}