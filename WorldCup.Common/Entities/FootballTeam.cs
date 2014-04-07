using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual ICollection<FootballMatch> HomeMatches { get; set; }
        public virtual ICollection<FootballMatch> AwayMatches { get; set; }

    }
}