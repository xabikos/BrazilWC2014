using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Newtonsoft.Json;

namespace WorldCup.Common.Entities
{
    /// <summary>
    /// The football team participating in the event
    /// </summary>
    public class FootballTeam
    {
        [JsonProperty(PropertyName = "key")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Name { get; set; }
        public string Code { get; set; }
        private string Flag { get; set; }

        [NotMapped]
        public Uri FlagUri
        {
            get { return new Uri(Flag); }
            set { Flag = value.AbsoluteUri; }
        }

        /// <summary>
        /// Internal class just to map the URI
        /// </summary>
        public class FootballTeamConfiguration : EntityTypeConfiguration<FootballTeam>
        {
            public FootballTeamConfiguration()
            {
                HasKey(ft => ft.Id);
                Property(ft => ft.Name).IsRequired().HasMaxLength(100);
                Property(ft => ft.Code).IsRequired().HasMaxLength(3);
                Property(ft => ft.Flag);
            }
        }

    }
}