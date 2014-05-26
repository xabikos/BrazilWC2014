using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WorldCup.Common.Entities;
using WorldCup.Models.Identity;

namespace WorldCup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            // Verifies that initialization logic would be executed only once
            if (context.Teams.Any()) return;

            InitializeAdmins(context);
            InitializeTeams(context);
            InitializeMatches(context);
        }

        private void InitializeTeams(ApplicationDbContext context)
        {
            var teams = new List<Team>
            {
                new Team {Id = "alg", Name = "Algeria", Code = "ALG"},
                new Team {Id = "arg", Name = "Argentina", Code = "ARG"},
                new Team {Id = "aus", Name = "Australia", Code = "AUS"},
                new Team {Id = "bel", Name = "Belgium", Code = "BEL"},
                new Team {Id = "bih", Name = "Bosnia-Herzegovina", Code = "BIH"},
                new Team {Id = "bra", Name = "Brazil", Code = "BRA"},
                new Team {Id = "chi", Name = "Chile", Code = "CHI"},
                new Team {Id = "civ", Name = "Cote d'Ivore", Code = "CIV"},
                new Team {Id = "cmr", Name = "Cameroon", Code = "CMR"},
                new Team {Id = "col", Name = "Colombia", Code = "COL"},
                new Team {Id = "crc", Name = "Costa Rica", Code = "CRC"},
                new Team {Id = "cro", Name = "Croatia", Code = "CRO"},
                new Team {Id = "ecu", Name = "Ecuador", Code = "ECU"},
                new Team {Id = "eng", Name = "England", Code = "ENG"},
                new Team {Id = "esp", Name = "Spain", Code = "ESP"},
                new Team {Id = "fra", Name = "France", Code = "FRA"},
                new Team {Id = "ger", Name = "Germany", Code = "GER"},
                new Team {Id = "gha", Name = "Ghana", Code = "Gha"},
                new Team {Id = "gre", Name = "Greece", Code = "GRE"},
                new Team {Id = "hon", Name = "Honduras", Code = "HON"},
                new Team {Id = "irn", Name = "Iran", Code = "IRN"},
                new Team {Id = "ita", Name = "Italy", Code = "ITA"},
                new Team {Id = "jpn", Name = "Japan", Code = "JPN"},
                new Team {Id = "kor", Name = "South Korea", Code = "KOR"},
                new Team {Id = "mex", Name = "Mexico", Code = "MEX"},
                new Team {Id = "ned", Name = "Netherlands", Code = "NED"},
                new Team {Id = "nga", Name = "Nigeria", Code = "NGA"},
                new Team {Id = "por", Name = "Portugal", Code = "POR"},
                new Team {Id = "rus", Name = "Russia", Code = "Rus"},
                new Team {Id = "sui", Name = "Switzerland", Code = "SUI"},
                new Team {Id = "uru", Name = "Uruguay", Code = "URU"},
                new Team {Id = "usa", Name = "United States", Code = "USA"}
            };

            context.Teams.AddRange(teams);
            context.SaveChanges();
        }

        private void InitializeMatches(ApplicationDbContext context)
        {
            const MatchState state = MatchState.Created;
            var matches = new List<Match>
            {
                new Match{HomeTeamId = "bra", AwayTeamId = "cro", State = state, Stage = MatchStage.GroupA, Date = new DateTime(2014,6,12, 20,0,0)},

                new Match{HomeTeamId = "mex", AwayTeamId = "cmr", State = state, Stage = MatchStage.GroupA, Date = new DateTime(2014,6,13, 16,0,0)},
                new Match{HomeTeamId = "esp", AwayTeamId = "ned", State = state, Stage = MatchStage.GroupB, Date = new DateTime(2014,6,13, 19,0,0)},
                new Match{HomeTeamId = "chi", AwayTeamId = "aus", State = state, Stage = MatchStage.GroupB, Date = new DateTime(2014,6,13, 22,0,0)},
                
                new Match{HomeTeamId = "col", AwayTeamId = "gre", State = state, Stage = MatchStage.GroupC, Date = new DateTime(2014,6,14, 16,0,0)},
                new Match{HomeTeamId = "uru", AwayTeamId = "crc", State = state, Stage = MatchStage.GroupD, Date = new DateTime(2014,6,14, 19,0,0)},
                new Match{HomeTeamId = "eng", AwayTeamId = "ita", State = state, Stage = MatchStage.GroupD, Date = new DateTime(2014,6,14, 22,0,0)},

                new Match{HomeTeamId = "civ", AwayTeamId = "jpn", State = state, Stage = MatchStage.GroupC, Date = new DateTime(2014,6,15, 1,0,0)},
                new Match{HomeTeamId = "sui", AwayTeamId = "ecu", State = state, Stage = MatchStage.GroupE, Date = new DateTime(2014,6,15, 16,0,0)},
                new Match{HomeTeamId = "fra", AwayTeamId = "hon", State = state, Stage = MatchStage.GroupE, Date = new DateTime(2014,6,15, 19,0,0)},
                new Match{HomeTeamId = "arg", AwayTeamId = "bih", State = state, Stage = MatchStage.GroupF, Date = new DateTime(2014,6,15, 22,0,0)},

                new Match{HomeTeamId = "ger", AwayTeamId = "por", State = state, Stage = MatchStage.GroupG, Date = new DateTime(2014,6,16, 16,0,0)},
                new Match{HomeTeamId = "irn", AwayTeamId = "nga", State = state, Stage = MatchStage.GroupF, Date = new DateTime(2014,6,16, 19,0,0)},
                new Match{HomeTeamId = "gha", AwayTeamId = "usa", State = state, Stage = MatchStage.GroupG, Date = new DateTime(2014,6,16, 22,0,0)},

                new Match{HomeTeamId = "bel", AwayTeamId = "alg", State = state, Stage = MatchStage.GroupH, Date = new DateTime(2014,6,17, 16,0,0)},
                new Match{HomeTeamId = "bra", AwayTeamId = "mex", State = state, Stage = MatchStage.GroupA, Date = new DateTime(2014,6,17, 19,0,0)},
                new Match{HomeTeamId = "rus", AwayTeamId = "kor", State = state, Stage = MatchStage.GroupH, Date = new DateTime(2014,6,17, 22,0,0)},

                new Match{HomeTeamId = "aus", AwayTeamId = "ned", State = state, Stage = MatchStage.GroupB, Date = new DateTime(2014,6,18, 16,0,0)},
                new Match{HomeTeamId = "esp", AwayTeamId = "chi", State = state, Stage = MatchStage.GroupB, Date = new DateTime(2014,6,18, 19,0,0)},
                new Match{HomeTeamId = "cmr", AwayTeamId = "cro", State = state, Stage = MatchStage.GroupA, Date = new DateTime(2014,6,18, 22,0,0)},

                new Match{HomeTeamId = "col", AwayTeamId = "civ", State = state, Stage = MatchStage.GroupC, Date = new DateTime(2014,6,19, 16,0,0)},
                new Match{HomeTeamId = "uru", AwayTeamId = "eng", State = state, Stage = MatchStage.GroupD, Date = new DateTime(2014,6,19, 19,0,0)},
                new Match{HomeTeamId = "jpn", AwayTeamId = "gre", State = state, Stage = MatchStage.GroupC, Date = new DateTime(2014,6,19, 22,0,0)},

                new Match{HomeTeamId = "ita", AwayTeamId = "crc", State = state, Stage = MatchStage.GroupD, Date = new DateTime(2014,6,20, 16,0,0)},
                new Match{HomeTeamId = "sui", AwayTeamId = "fra", State = state, Stage = MatchStage.GroupE, Date = new DateTime(2014,6,20, 19,0,0)},
                new Match{HomeTeamId = "hon", AwayTeamId = "ecu", State = state, Stage = MatchStage.GroupE, Date = new DateTime(2014,6,20, 22,0,0)},

                new Match{HomeTeamId = "arg", AwayTeamId = "irn", State = state, Stage = MatchStage.GroupF, Date = new DateTime(2014,6,21, 16,0,0)},
                new Match{HomeTeamId = "ger", AwayTeamId = "gha", State = state, Stage = MatchStage.GroupG, Date = new DateTime(2014,6,21, 19,0,0)},
                new Match{HomeTeamId = "nga", AwayTeamId = "bih", State = state, Stage = MatchStage.GroupF, Date = new DateTime(2014,6,21, 22,0,0)},

                new Match{HomeTeamId = "bel", AwayTeamId = "rus", State = state, Stage = MatchStage.GroupH, Date = new DateTime(2014,6,22, 16,0,0)},
                new Match{HomeTeamId = "kor", AwayTeamId = "alg", State = state, Stage = MatchStage.GroupH, Date = new DateTime(2014,6,22, 19,0,0)},
                new Match{HomeTeamId = "usa", AwayTeamId = "por", State = state, Stage = MatchStage.GroupG, Date = new DateTime(2014,6,22, 22,0,0)},

                new Match{HomeTeamId = "ned", AwayTeamId = "chi", State = state, Stage = MatchStage.GroupB, Date = new DateTime(2014,6,23, 16,0,0)},
                new Match{HomeTeamId = "aus", AwayTeamId = "esp", State = state, Stage = MatchStage.GroupB, Date = new DateTime(2014,6,23, 16,0,0)},
                new Match{HomeTeamId = "cmr", AwayTeamId = "bra", State = state, Stage = MatchStage.GroupA, Date = new DateTime(2014,6,23, 20,0,0)},
                new Match{HomeTeamId = "cro", AwayTeamId = "mex", State = state, Stage = MatchStage.GroupA, Date = new DateTime(2014,6,23, 20,0,0)},

                new Match{HomeTeamId = "ita", AwayTeamId = "uru", State = state, Stage = MatchStage.GroupD, Date = new DateTime(2014,6,24, 16,0,0)},
                new Match{HomeTeamId = "crc", AwayTeamId = "eng", State = state, Stage = MatchStage.GroupD, Date = new DateTime(2014,6,24, 16,0,0)},
                new Match{HomeTeamId = "jpn", AwayTeamId = "col", State = state, Stage = MatchStage.GroupC, Date = new DateTime(2014,6,24, 20,0,0)},
                new Match{HomeTeamId = "gre", AwayTeamId = "civ", State = state, Stage = MatchStage.GroupC, Date = new DateTime(2014,6,24, 20,0,0)},

                new Match{HomeTeamId = "nga", AwayTeamId = "arg", State = state, Stage = MatchStage.GroupF, Date = new DateTime(2014,6,25, 16,0,0)},
                new Match{HomeTeamId = "bih", AwayTeamId = "irn", State = state, Stage = MatchStage.GroupF, Date = new DateTime(2014,6,25, 16,0,0)},
                new Match{HomeTeamId = "hon", AwayTeamId = "sui", State = state, Stage = MatchStage.GroupE, Date = new DateTime(2014,6,25, 20,0,0)},
                new Match{HomeTeamId = "ecu", AwayTeamId = "fra", State = state, Stage = MatchStage.GroupE, Date = new DateTime(2014,6,25, 20,0,0)},

                new Match{HomeTeamId = "por", AwayTeamId = "gha", State = state, Stage = MatchStage.GroupG, Date = new DateTime(2014,6,26, 16,0,0)},
                new Match{HomeTeamId = "usa", AwayTeamId = "ger", State = state, Stage = MatchStage.GroupG, Date = new DateTime(2014,6,26, 16,0,0)},
                new Match{HomeTeamId = "kor", AwayTeamId = "bel", State = state, Stage = MatchStage.GroupH, Date = new DateTime(2014,6,26, 20,0,0)},
                new Match{HomeTeamId = "alg", AwayTeamId = "rus", State = state, Stage = MatchStage.GroupH, Date = new DateTime(2014,6,26, 20,0,0)}
            };
             
            context.Matches.AddRange(matches);
            context.SaveChanges();
        }

        private void InitializeAdmins(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));

            const string xabikosName = "c.karypidis@niposoftware.com";
            const string rutgerName = "r.dejong@niposoftware.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                roleManager.Create(role);
            }

            var xabikosUser = userManager.FindByName(xabikosName);
            if (xabikosUser == null)
            {
                xabikosUser = new ApplicationUser
                {
                    UserName = xabikosName,
                    Email = xabikosName,
                    FirstName = "Charalampos",
                    LastName = "Karypidis",
                    RegistrationDate = DateTime.UtcNow
                };
                userManager.Create(xabikosUser, password);
                userManager.SetLockoutEnabled(xabikosUser.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(xabikosUser.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                userManager.AddToRole(xabikosUser.Id, role.Name);
            }

            var rutgerUser = userManager.FindByName(rutgerName);
            if (rutgerUser == null)
            {
                rutgerUser = new ApplicationUser
                {
                    UserName = rutgerName,
                    Email = rutgerName,
                    FirstName = "Rutger",
                    LastName = "de Jong",
                    RegistrationDate = DateTime.UtcNow
                };
                userManager.Create(rutgerUser, password);
                userManager.SetLockoutEnabled(rutgerUser.Id, false);
            }

            // Add user admin to Role Admin if not already added
            rolesForUser = userManager.GetRoles(rutgerUser.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                userManager.AddToRole(rutgerUser.Id, role.Name);
            }

        }

    }
}