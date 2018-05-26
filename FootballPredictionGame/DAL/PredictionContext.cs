using FootballPredictionGame.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FootballPredictionGame.DAL
{
    public class PredictionContext : DbContext
    {

        public PredictionContext() : base("PredictionContext")
        {
        }

        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<Predictor> Predictors { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}