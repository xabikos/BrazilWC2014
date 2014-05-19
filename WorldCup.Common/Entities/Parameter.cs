using System;
using System.ComponentModel.DataAnnotations;

namespace WorldCup.Common.Entities
{
    /// <summary>
    /// Represents a parameter for the system.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Unique identifier for the setting
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// The value as a string. It can contain anything (e.g. number, json object)
        /// </summary>
        [Required]
        public string Value { get; set; }

    }
}