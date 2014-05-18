using System;
using System.ComponentModel.DataAnnotations;

namespace WorldCup.Common.Entities
{
    /// <summary>
    /// Holds info about the collected money  
    /// </summary>
    public class RaisedMoney
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Range(0, 10000)]
        public int Amount { get; set; }

    }
}