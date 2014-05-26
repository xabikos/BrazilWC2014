using System;
using System.Collections.Generic;

namespace WorldCup.Common
{
    public static class PredefinedParameters
    {
        public const string NavigationBrandText = "NavigationBrandText";
        public const string ApplicationLogo = "ApplicationLogo";
        public const string ApplicationLogoText = "ApplicationLogoText";
        public const string IntroductionText = "IntroductionText";
        public const string UnicefChampion = "UnicefChampion";
        public const string PlayingFee = "PlayingFee";
        public const string LastUpdateTime = "LastUpdateTime";

// ReSharper disable once InconsistentNaming
        private static readonly List<string> _parameters = new List<string>
        {
            NavigationBrandText,
            ApplicationLogo,
            ApplicationLogoText,
            IntroductionText,
            UnicefChampion,
            PlayingFee,
            LastUpdateTime
        };

        public static IEnumerable<string> Parameters
        {
            get { return _parameters; }
        }

    }
}