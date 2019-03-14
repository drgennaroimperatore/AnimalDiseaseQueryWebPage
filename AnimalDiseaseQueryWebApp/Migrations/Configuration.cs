namespace AnimalDiseaseQueryWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AnimalDiseaseQueryWebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AnimalDiseaseQueryWebApp.Models.ADDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            
        }

        protected override void Seed(AnimalDiseaseQueryWebApp.Models.ADDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            
        }

        
    }
}