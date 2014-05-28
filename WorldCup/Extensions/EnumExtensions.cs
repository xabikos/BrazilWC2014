using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WorldCup.Extensions
{
    /// <summary>
    /// Contains extensions methods for Enum types
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Search if the enum value has a custom display attribute and displays it
        /// </summary>
        public static string DisplayName(this Enum value)
        {
            var enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            var member = enumType.GetMember(enumValue)[0];
            string outString;

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attrs.Any())
            {
                var displayAttr = ((DisplayAttribute)attrs[0]);

                outString = displayAttr.Name;

                if (displayAttr.ResourceType != null)
                {
                    outString = displayAttr.GetName();
                }
            }
            else
            {
                outString = value.ToString();
            }

            return outString; 
        }

    }
}