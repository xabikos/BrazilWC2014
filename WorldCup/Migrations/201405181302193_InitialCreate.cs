namespace WorldCup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LongRunningResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecondStageTeams = c.String(),
                        QuarterFinalTeams = c.String(),
                        SemiFinalTeams = c.String(),
                        SmallFinalTeams = c.String(),
                        FinalTeams = c.String(),
                        WinnerTeamId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.WinnerTeamId)
                .Index(t => t.WinnerTeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        State = c.Int(nullable: false),
                        Stage = c.Int(nullable: false),
                        HomeTeamId = c.String(nullable: false, maxLength: 128),
                        AwayTeamId = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Result = c.Int(nullable: false),
                        HomeTeamHalfTimeGoals = c.Int(nullable: false),
                        AwayTeamHalfTimeGoals = c.Int(nullable: false),
                        HomeTeamFullTimeGoals = c.Int(nullable: false),
                        AwayTeamFullTimeGoals = c.Int(nullable: false),
                        YellowCards = c.Int(nullable: false),
                        RedCards = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeamId)
                .ForeignKey("dbo.Teams", t => t.HomeTeamId)
                .Index(t => new { t.HomeTeamId, t.AwayTeamId }, unique: true, name: "UN_MATCH_HOMETEAMAWAYTEAM");
            
            CreateTable(
                "dbo.MatchPoints",
                c => new
                    {
                        MatchPointsId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        MatchId = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchPointsId)
                .ForeignKey("dbo.Matches", t => t.MatchId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.UserId, t.MatchId }, unique: true, name: "UN_MATCHPOINTS_USER_MATCH");
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LongRunningPoints",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        SecondStagePoints = c.Int(nullable: false),
                        QuarterFinalPoints = c.Int(nullable: false),
                        SemiFinalPoints = c.Int(nullable: false),
                        SmallFinalPoints = c.Int(nullable: false),
                        FinalPoints = c.Int(nullable: false),
                        WinnerPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LongRunningPredictions",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        SecondStageTeams = c.String(),
                        QuarterFinalTeams = c.String(),
                        SemiFinalTeams = c.String(),
                        SmallFinalTeams = c.String(),
                        FinalTeams = c.String(),
                        WinnerTeamId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.WinnerTeamId)
                .Index(t => t.UserId)
                .Index(t => t.WinnerTeamId);
            
            CreateTable(
                "dbo.MatchPredictions",
                c => new
                    {
                        MatchPredictionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        MatchId = c.Int(nullable: false),
                        Result = c.Int(nullable: false),
                        HomeTeamHalfTimeGoals = c.Int(nullable: false),
                        AwayTeamHalfTimeGoals = c.Int(nullable: false),
                        HomeTeamFullTimeGoals = c.Int(nullable: false),
                        AwayTeamFullTimeGoals = c.Int(nullable: false),
                        YellowCards = c.Int(nullable: false),
                        RedCards = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchPredictionId)
                .ForeignKey("dbo.Matches", t => t.MatchId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.UserId, t.MatchId }, unique: true, name: "UN_MATCHPREDICTION_USER_MATCH");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RaisedMoney",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Date, unique: true, name: "UN_RAISEDMONEY_DATE");
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SystemParameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupHalfTimeScoreFactor = c.Int(nullable: false),
                        GroupFullTimeScoreFactor = c.Int(nullable: false),
                        GroupWinnerFactor = c.Int(nullable: false),
                        GroupYellowCardsNumberFactor = c.Int(nullable: false),
                        GroupRedCardsNumberFactor = c.Int(nullable: false),
                        FinalsHalfTimeScoreFactor = c.Int(nullable: false),
                        FinalsFullTimeScoreFactor = c.Int(nullable: false),
                        FinalsWinnerFactor = c.Int(nullable: false),
                        FinalsYellowCardsNumberFactor = c.Int(nullable: false),
                        FinalsRedCardsNumberFactor = c.Int(nullable: false),
                        Round16TeamsFactor = c.Int(nullable: false),
                        QuarterFinalTeamsFactor = c.Int(nullable: false),
                        SemiFinalTeamsFactor = c.Int(nullable: false),
                        SmallFinalTeamsFactor = c.Int(nullable: false),
                        FinalTeamsFactor = c.Int(nullable: false),
                        WinnerTeamFactor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MatchPoints", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MatchPredictions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MatchPredictions", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.LongRunningPredictions", "WinnerTeamId", "dbo.Teams");
            DropForeignKey("dbo.LongRunningPredictions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LongRunningPoints", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MatchPoints", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.LongRunningResults", "WinnerTeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "HomeTeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "AwayTeamId", "dbo.Teams");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RaisedMoney", "UN_RAISEDMONEY_DATE");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.MatchPredictions", "UN_MATCHPREDICTION_USER_MATCH");
            DropIndex("dbo.LongRunningPredictions", new[] { "WinnerTeamId" });
            DropIndex("dbo.LongRunningPredictions", new[] { "UserId" });
            DropIndex("dbo.LongRunningPoints", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.MatchPoints", "UN_MATCHPOINTS_USER_MATCH");
            DropIndex("dbo.Matches", "UN_MATCH_HOMETEAMAWAYTEAM");
            DropIndex("dbo.LongRunningResults", new[] { "WinnerTeamId" });
            DropTable("dbo.SystemParameters");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RaisedMoney");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.MatchPredictions");
            DropTable("dbo.LongRunningPredictions");
            DropTable("dbo.LongRunningPoints");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.MatchPoints");
            DropTable("dbo.Matches");
            DropTable("dbo.Teams");
            DropTable("dbo.LongRunningResults");
        }
    }
}
