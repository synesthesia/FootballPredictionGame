using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FootballPredictionGame.Models;


namespace FootballPredictionGame.DAL
{
    public class PredictionInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PredictionContext>
    {
        protected override void Seed(PredictionContext context)
        {
            var predictors = new List<Predictor>
            {
                new Predictor{FirstName="Carson",LastName="Alexander"},
                new Predictor{FirstName="Meredith",LastName="Alonso"},
                new Predictor{FirstName="Arturo",LastName="Anand"},
                new Predictor{FirstName="Gytis",LastName="Barzdukas"},
                new Predictor{FirstName="Yan",LastName="Li"},
                new Predictor{FirstName="Peggy",LastName="Justice"},
                new Predictor{FirstName="Laura",LastName="Norman"},
                new Predictor{FirstName="Nino",LastName="Olivetto"}
            };

            predictors.ForEach(s => context.Predictors.Add(s));
            context.SaveChanges();

            var teams = new List<Team>
            {
                new Team{Name="England"},
                new Team{Name="France"},
                new Team{Name="Spain"},
                new Team{Name="Italy"},
                new Team{Name="Brazil"},
                new Team{Name="Argentina"},
                new Team{Name="Germany"},
                new Team{Name="Portugal"},
                new Team{Name="Chile"},
                new Team{Name="Belgium"},
                new Team{Name="Mexico"},
                new Team{Name="USA"},
                new Team{Name="China"},
                new Team{Name="Japan"},
                new Team{Name="South Africa"},
                new Team{Name="Nigeria"}
            };
            teams.ForEach(s => context.Teams.Add(s));
            context.SaveChanges();

            var fixtures = new List<Fixture>
            {
                new Fixture{HomeTeamId=1,AwayTeamId=2,GameDate=DateTime.Parse("2018-07-01")},
                new Fixture{HomeTeamId=3,AwayTeamId=4,GameDate=DateTime.Parse("2018-07-01")},
                new Fixture{HomeTeamId=5,AwayTeamId=6,GameDate=DateTime.Parse("2018-07-01")},
                new Fixture{HomeTeamId=7,AwayTeamId=8,GameDate=DateTime.Parse("2018-07-02")},
                new Fixture{HomeTeamId=9,AwayTeamId=10,GameDate=DateTime.Parse("2018-07-02")},
                new Fixture{HomeTeamId=11,AwayTeamId=12,GameDate=DateTime.Parse("2018-07-02")},
                new Fixture{HomeTeamId=13,AwayTeamId=14,GameDate=DateTime.Parse("2018-07-02")},
                new Fixture{HomeTeamId=15,AwayTeamId=16,GameDate=DateTime.Parse("2018-07-03")}
            };
            fixtures.ForEach(s => context.Fixtures.Add(s));
            context.SaveChanges();
        }
    }
}