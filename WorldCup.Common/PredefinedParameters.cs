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

// ReSharper disable once InconsistentNaming
        private static readonly List<string> _parameters = new List<string>
        {
            NavigationBrandText,
            ApplicationLogo,
            ApplicationLogoText,
            IntroductionText
        };
        public static IEnumerable<string> Parameters
        {
            get { return _parameters; }
        }

    }
}