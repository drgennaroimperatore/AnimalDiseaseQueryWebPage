namespace AnimalDiseaseQueryWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Sex = c.String(unicode: false),
                        Age = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Likelihoods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(unicode: false),
                        AnimalId = c.Int(nullable: false),
                        SignId = c.Int(nullable: false),
                        DiseaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animals", t => t.AnimalId, cascadeDelete: true)
                .ForeignKey("dbo.Diseases", t => t.DiseaseId, cascadeDelete: true)
                .ForeignKey("dbo.Signs", t => t.SignId, cascadeDelete: true)
                .Index(t => t.AnimalId)
                .Index(t => t.SignId)
                .Index(t => t.DiseaseId);
            
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Treatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Info = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Signs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type_of_Value = c.Int(nullable: false),
                        Probability = c.String(unicode: false),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TreatmentDiseases",
                c => new
                    {
                        Treatment_Id = c.Int(nullable: false),
                        Disease_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Treatment_Id, t.Disease_Id })
                .ForeignKey("dbo.Treatments", t => t.Treatment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Diseases", t => t.Disease_Id, cascadeDelete: true)
                .Index(t => t.Treatment_Id)
                .Index(t => t.Disease_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likelihoods", "SignId", "dbo.Signs");
            DropForeignKey("dbo.TreatmentDiseases", "Disease_Id", "dbo.Diseases");
            DropForeignKey("dbo.TreatmentDiseases", "Treatment_Id", "dbo.Treatments");
            DropForeignKey("dbo.Likelihoods", "DiseaseId", "dbo.Diseases");
            DropForeignKey("dbo.Likelihoods", "AnimalId", "dbo.Animals");
            DropIndex("dbo.TreatmentDiseases", new[] { "Disease_Id" });
            DropIndex("dbo.TreatmentDiseases", new[] { "Treatment_Id" });
            DropIndex("dbo.Likelihoods", new[] { "DiseaseId" });
            DropIndex("dbo.Likelihoods", new[] { "SignId" });
            DropIndex("dbo.Likelihoods", new[] { "AnimalId" });
            DropTable("dbo.TreatmentDiseases");
            DropTable("dbo.Signs");
            DropTable("dbo.Treatments");
            DropTable("dbo.Diseases");
            DropTable("dbo.Likelihoods");
            DropTable("dbo.Animals");
        }
    }
}
