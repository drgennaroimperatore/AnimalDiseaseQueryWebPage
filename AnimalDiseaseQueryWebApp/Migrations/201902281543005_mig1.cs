namespace AnimalDiseaseQueryWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriorsDiseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Probability = c.String(unicode: false),
                        Disease_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diseases", t => t.Disease_Id)
                .Index(t => t.Disease_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriorsDiseases", "Disease_Id", "dbo.Diseases");
            DropIndex("dbo.PriorsDiseases", new[] { "Disease_Id" });
            DropTable("dbo.PriorsDiseases");
        }
    }
}
