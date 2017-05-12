using System.Data.Entity.Migrations;

namespace ColleageInnerTraining.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ColleageInnerTraining.EntityFramework.ColleageInnerTrainingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ColleageInnerTraining";
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(ColleageInnerTraining.EntityFramework.ColleageInnerTrainingDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
