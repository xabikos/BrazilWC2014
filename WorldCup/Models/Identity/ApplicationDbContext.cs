using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            modelBuilder.Configurations.Add(new SecondStageStatisticsConfiguration());
            modelBuilder.Configurations.Add(new QuarterFinalStatisticsConfiguration());
            modelBuilder.Configurations.Add(new SemiFinalStatisticsConfiguration());
            modelBuilder.Configurations.Add(new SmallFinalStatisticsConfiguration());
            modelBuilder.Configurations.Add(new FinalStatisticsConfiguration());
            modelBuilder.Configurations.Add(new WinnerStatisticsConfiguration());

            modelBuilder.Entity<ApplicationUser>().Ignore(au => au.FullName);

            base.OnModelCreating(modelBuilder);
        }

        #region DbSets

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchPrediction> MatchesPredictions { get; set; }
        public DbSet<SystemParameters> SystemParameters { get; set; }
        public DbSet<MatchPoints> MatchPoints { get; set; }
        public DbSet<LongRunningResults> LongRunningResults { get; set; }
        public DbSet<RaisedMoney> RaisedMoney { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<SecondStageStatistics> SecondStageStatistics { get; set; }
        public DbSet<QuarterFinalStatistics> QuarterFinalStatistics { get; set; }
        public DbSet<SemiFinalStatistics> SemiFinalStatistics { get; set; }
        public DbSet<SmallFinalStatistics> SmallFinalStatistics { get; set; }
        public DbSet<FinalStatistics> FinalStatistics { get; set; }
        public DbSet<WinnerStatistics> WinnerStatistics { get; set; }

        #endregion

    }
}