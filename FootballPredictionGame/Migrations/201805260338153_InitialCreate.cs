namespace FootballPredictionGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fixture",
                c => new
                    {
                        FixtureId = c.Int(nullable: false, identity: true),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        GameDate = c.DateTime(nullable: false),
                        HomeResult = c.Int(nullable: false),
                        AwayResult = c.Int(nullable: false),
                        Team_TeamId = c.Int(),
                        Team_TeamId1 = c.Int(),
                    })
                .PrimaryKey(t => t.FixtureId)
                .ForeignKey("dbo.Team", t => t.Team_TeamId)
                .ForeignKey("dbo.Team", t => t.Team_TeamId1)
                .ForeignKey("dbo.Team", t => t.AwayTeamId)
                .ForeignKey("dbo.Team", t => t.HomeTeamId)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId)
                .Index(t => t.Team_TeamId)
                .Index(t => t.Team_TeamId1);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Flag = c.String(),
                    })
                .PrimaryKey(t => t.TeamId);
            
            CreateTable(
                "dbo.Prediction",
                c => new
                    {
                        PredictionId = c.Int(nullable: false, identity: true),
                        PredictorId = c.Int(nullable: false),
                        FixtureId = c.Int(nullable: false),
                        HomeResult = c.Int(nullable: false),
                        AwayResult = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PredictionId)
                .ForeignKey("dbo.Fixture", t => t.FixtureId)
                .ForeignKey("dbo.Predictor", t => t.PredictorId)
                .Index(t => t.PredictorId)
                .Index(t => t.FixtureId);
            
            CreateTable(
                "dbo.Predictor",
                c => new
                    {
                        PredictorId = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PredictorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prediction", "PredictorId", "dbo.Predictor");
            DropForeignKey("dbo.Prediction", "FixtureId", "dbo.Fixture");
            DropForeignKey("dbo.Fixture", "HomeTeamId", "dbo.Team");
            DropForeignKey("dbo.Fixture", "AwayTeamId", "dbo.Team");
            DropForeignKey("dbo.Fixture", "Team_TeamId1", "dbo.Team");
            DropForeignKey("dbo.Fixture", "Team_TeamId", "dbo.Team");
            DropIndex("dbo.Prediction", new[] { "FixtureId" });
            DropIndex("dbo.Prediction", new[] { "PredictorId" });
            DropIndex("dbo.Fixture", new[] { "Team_TeamId1" });
            DropIndex("dbo.Fixture", new[] { "Team_TeamId" });
            DropIndex("dbo.Fixture", new[] { "AwayTeamId" });
            DropIndex("dbo.Fixture", new[] { "HomeTeamId" });
            DropTable("dbo.Predictor");
            DropTable("dbo.Prediction");
            DropTable("dbo.Team");
            DropTable("dbo.Fixture");
        }
    }
}
