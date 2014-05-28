using System;

namespace WorldCup.Extensions
{
    /// <summary>
    /// Contains extensions methods for string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the team name base on the code
        /// </summary>
        public static string GetTeamName(this string teamCode)
        {
            switch(teamCode.ToLowerInvariant())
            {
                case "alg":
                    return "Algeria";
                case "arg":
                    return "Argentina";
                case "aus":
                    return "Australia";
                case "bel":
                    return "Belgium";
                case "bih":
                    return "Bosnia-Herzegovina";
                case "bra":
                    return "Brazil";
                case "chi":
                    return "Chile";
                case "civ":
                    return "Cote d'Ivore";
                case "cmr":
                    return "Cameroon";
                case "col":
                    return "Colombia";
                case "crc":
                    return "Costa Rica";
                case "cro":
                    return "Croatia";
                case "ecu":
                    return "Ecuador";
                case "eng":
                    return "England";
                case "esp":
                    return "Spain";
                case "fra":
                    return "France";
                case "ger":
                    return "Germany";
                case "gha":
                    return "Ghana";
                case "gre":
                    return "Greece";
                case "hon":
                    return "Honduras";
                case "irn":
                    return "Iran";
                case "ita":
                    return "Italy";
                case "jpn":
                    return "Japan";
                case "kor":
                    return "South Korea";
                case "mex":
                    return "Mexico";
                case "ned":
                    return "Netherlands";
                case "nga":
                    return "Nigeria";
                case "por":
                    return "Portugal";
                case "rus":
                    return "Russia";
                case "sui":
                    return "Switzerland";
                case "uru":
                    return "Uruguay";
                case "usa":
                    return "United States";
                default:
                    return "No valid code";
            }
        }

    }
}