namespace FootballPredictionGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PredictorEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Predictor", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Predictor", "Email");
        }
    }
}
