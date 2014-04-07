using System;

namespace WorldCup.Common.Entities
{
    public class Player
    {
        public Player()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Old { get; set; }
        public string TeamId { get; set; }
        public Team Team { get; set; }

    }
}