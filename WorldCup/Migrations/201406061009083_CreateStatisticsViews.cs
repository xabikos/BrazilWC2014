namespace WorldCup.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class CreateStatisticsViews : DbMigration
	{
		public override void Up()
		{
			// Second stage statistics view
			var sql = @"create view SecondStageStatistics as
							select Teams.Code, count(LongRunningPredictions.SecondStageTeams)  as Count
							from Teams left join LongRunningPredictions on 
							LongRunningPredictions.SecondStageTeams like '%' + Teams.Code + '%'
							group by Teams.Code";
			sql = sql.Replace("'", "''");
			Sql(string.Format("EXECUTE sp_executesql N'{0}'", sql));

			// Quarter final statistics
			sql = @"create view QuarterFinalStatistics as
							select Teams.Code, count(LongRunningPredictions.QuarterFinalTeams)  as Count
							from Teams left join LongRunningPredictions on 
							LongRunningPredictions.QuarterFinalTeams like '%' + Teams.Code + '%'
							group by Teams.Code";
			sql = sql.Replace("'", "''");
			Sql(string.Format("EXECUTE sp_executesql N'{0}'", sql));

			// Semi final statistics
			sql = @"create view SemiFinalStatistics as
							select Teams.Code, count(LongRunningPredictions.SemiFinalTeams)  as Count
							from Teams left join LongRunningPredictions on 
							LongRunningPredictions.SemiFinalTeams like '%' + Teams.Code + '%'
							group by Teams.Code";
			sql = sql.Replace("'", "''");
			Sql(string.Format("EXECUTE sp_executesql N'{0}'", sql));

			// Small final statistics
			sql = @"create view SmallFinalStatistics as
							select Teams.Code, count(LongRunningPredictions.SmallFinalTeams)  as Count
							from Teams left join LongRunningPredictions on 
							LongRunningPredictions.SmallFinalTeams like '%' + Teams.Code + '%'
							group by Teams.Code";
			sql = sql.Replace("'", "''");
			Sql(string.Format("EXECUTE sp_executesql N'{0}'", sql));

			// Final statistics
			sql = @"create view FinalStatistics as
							select Teams.Code, count(LongRunningPredictions.FinalTeams)  as Count
							from Teams left join LongRunningPredictions on 
							LongRunningPredictions.FinalTeams like '%' + Teams.Code + '%'
							group by Teams.Code";
			sql = sql.Replace("'", "''");
			Sql(string.Format("EXECUTE sp_executesql N'{0}'", sql));

			// Winner statistics
			sql = @"create view WinnerStatistics as
							select Teams.Id as Code, count(LongRunningPredictions.WinnerTeamId)  as Count
							from Teams left join LongRunningPredictions on 
							Teams.Id = LongRunningPredictions.WinnerTeamId
							group by Teams.Id";
			sql = sql.Replace("'", "''");
			Sql(string.Format("EXECUTE sp_executesql N'{0}'", sql));
			
		}
		
		public override void Down()
		{
			Sql(@"IF  OBJECT_ID('dbo.SecondStageStatistics') IS NOT NULL
					DROP VIEW dbo.SecondStageStatistics");
			Sql(@"IF  OBJECT_ID('dbo.QuarterFinalStatistics') IS NOT NULL
					DROP VIEW dbo.QuarterFinalStatistics");
			Sql(@"IF  OBJECT_ID('dbo.SemiFinalStatistics') IS NOT NULL
					DROP VIEW dbo.SemiFinalStatistics");
			Sql(@"IF  OBJECT_ID('dbo.SmallFinalStatistics') IS NOT NULL
					DROP VIEW dbo.SmallFinalStatistics");
			Sql(@"IF  OBJECT_ID('dbo.FinalStatistics') IS NOT NULL
				   DROP VIEW dbo.FinalStatistics");
			Sql(@"IF  OBJECT_ID('dbo.WinnerStatistics') IS NOT NULL
				   DROP VIEW dbo.WinnerStatistics");
		}

	}
}