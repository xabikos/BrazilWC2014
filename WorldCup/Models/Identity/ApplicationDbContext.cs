using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WorldCup.Common.DataAccess;
using WorldCup.Common.Entities;
using WorldCup.Migrations;

namespace WorldCup.Models.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LongRunningPredictionConfiguration());
            modelBuilder.Configurations.Add(new FootballTeamConfiguration());
            modelBuilder.Configurations.Add(new MatchConfiguration());
            modelBuilder.Configurations.Add(new MatchPredictionConfiguration());
            modelBuilder.Configurations.Add(new MatchPointsConfiguration());
            modelBuilder.Configurations.Add(new LongRunningPointsConfiguration());
            modelBuilder.Configurations.Add(new RaisedMoneyConfiguration());
            modelBuilder.Configurations.Add(new ParametersConfiguration());

            modelBuilder.Entity<ApplicationUser>().Ignore(au => au.FullName);

            base.OnModelCreating(modelBuilder);
        }

        #region DbSets

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<SystemParameters> SystemParameters { get; set; }
        public DbSet<MatchPoints> MatchPoints { get; set; }
        public DbSet<LongRunningResults> LongRunningResults { get; set; }
        public DbSet<RaisedMoney> RaisedMoney { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        #endregion

    }

}