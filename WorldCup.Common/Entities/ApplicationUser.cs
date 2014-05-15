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

            if(LongRunningPoints != null)
            {
                // First stage date ended so second stage teams are known
                if(date > new DateTime(2014, 6, 26))
                {
                    result += LongRunningPoints.SecondStagePoints;
                }

                // Second stage date ended so quarter final teams are known 
                if (date > new DateTime(2014, 7, 1))
                {
                    result += LongRunningPoints.QuarterFinalPoints;
                }

                // Quarter final date ended so semi final teams are known
                if (date > new DateTime(2014, 7, 5))
                {
                    result += LongRunningPoints.SemiFinalPoints;
                }

                // Semi final date ended so small final and final teams are known
                if (date > new DateTime(2014, 7, 9))
                {
                    result += LongRunningPoints.SmallFinalPoints;
                    result += LongRunningPoints.FinalPoints;
                }

                // Tournament ended so winner is known
                if(date > new DateTime(2014, 7, 13))
                {
                    result += LongRunningPoints.WinnerPoints;
                }
            }

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