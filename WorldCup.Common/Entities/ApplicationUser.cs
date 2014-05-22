using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WorldCup.Common.Entities
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual LongRunningPrediction LongRunningPrediction { get; set; }
        public virtual LongRunningPoints LongRunningPoints { get; set; }

        public virtual ICollection<MatchPrediction> MatchPredictions { get; set; }
        public virtual ICollection<MatchPoints> MatchPoints { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public int GetPointsForDate(DateTime date)
        {
            var result = 0;
            result += MatchPoints.Where(m => m.Match.Date <= date).Sum(mp => mp.Points);

            if (LongRunningPoints == null) 
                return result;

            result += LongRunningPoints.SecondStagePoints;
            result += LongRunningPoints.QuarterFinalPoints;
            result += LongRunningPoints.SemiFinalPoints;
            result += LongRunningPoints.SmallFinalPoints;
            result += LongRunningPoints.FinalPoints;
            result += LongRunningPoints.WinnerPoints;

            return result;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}