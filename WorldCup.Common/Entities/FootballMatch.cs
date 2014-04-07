using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WorldCup.Common.Entities
{
    public class FootballMatch
    {
        public virtual FootballTeam HomeTeam { get; set; }
        
        [JsonProperty(PropertyName = "team1_key")]
        public string HomeTeamId { get; set; }
        
        public virtual FootballTeam AwayTeam { get; set; }
        
        [JsonProperty(PropertyName = "team2_key")]
        public string AwayTeamId { get; set; }

        [JsonProperty(PropertyName = "play_at")]
        [Required]
        public DateTime Date { get; set; }
       
    }
}