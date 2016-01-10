namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmergencyContact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        StreetAddress = c.String(),
                        City = c.String(),
                        County = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        Patron_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patron", t => t.Patron_ID)
                .Index(t => t.Patron_ID);
            
            CreateTable(
                "dbo.Ethnicity",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Gender",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Patron",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        NumberInHousehold = c.Short(nullable: false),
                        Banned = c.Boolean(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        Ethnicity_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ethnicity", t => t.Ethnicity_ID)
                .Index(t => t.Ethnicity_ID);
            
            CreateTable(
                "dbo.ServiceEligibility",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        Patron_ID = c.Int(nullable: false),
                        ServiceType_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patron", t => t.Patron_ID, cascadeDelete: true)
                .ForeignKey("dbo.ServiceType", t => t.ServiceType_ID, cascadeDelete: true)
                .Index(t => t.Patron_ID)
                .Index(t => t.ServiceType_ID);
            
            CreateTable(
                "dbo.ServiceType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(nullable: false),
                        ServiceDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Race",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ResidenceStatus",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        RoleName = c.String(),
                        RoleDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleVolunteer",
                c => new
                    {
                        VolunteerUsername = c.String(nullable: false, maxLength: 128),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VolunteerUsername, t.RoleID })
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Volunteer", t => t.VolunteerUsername, cascadeDelete: true)
                .Index(t => t.VolunteerUsername)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Volunteer",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        HashedPassword = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Username)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "dbo.Visit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        CreateVolunteer_Username = c.String(maxLength: 128),
                        Patron_ID = c.Int(),
                        Service_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Volunteer", t => t.CreateVolunteer_Username)
                .ForeignKey("dbo.Patron", t => t.Patron_ID)
                .ForeignKey("dbo.ServiceType", t => t.Service_ID)
                .Index(t => t.CreateVolunteer_Username)
                .Index(t => t.Patron_ID)
                .Index(t => t.Service_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visit", "Service_ID", "dbo.ServiceType");
            DropForeignKey("dbo.Visit", "Patron_ID", "dbo.Patron");
            DropForeignKey("dbo.Visit", "CreateVolunteer_Username", "dbo.Volunteer");
            DropForeignKey("dbo.RoleVolunteer", "VolunteerUsername", "dbo.Volunteer");
            DropForeignKey("dbo.RoleVolunteer", "RoleID", "dbo.Role");
            DropForeignKey("dbo.ServiceEligibility", "ServiceType_ID", "dbo.ServiceType");
            DropForeignKey("dbo.ServiceEligibility", "Patron_ID", "dbo.Patron");
            DropForeignKey("dbo.Patron", "Ethnicity_ID", "dbo.Ethnicity");
            DropForeignKey("dbo.EmergencyContact", "Patron_ID", "dbo.Patron");
            DropIndex("dbo.Visit", new[] { "Service_ID" });
            DropIndex("dbo.Visit", new[] { "Patron_ID" });
            DropIndex("dbo.Visit", new[] { "CreateVolunteer_Username" });
            DropIndex("dbo.Volunteer", new[] { "Username" });
            DropIndex("dbo.RoleVolunteer", new[] { "RoleID" });
            DropIndex("dbo.RoleVolunteer", new[] { "VolunteerUsername" });
            DropIndex("dbo.ServiceEligibility", new[] { "ServiceType_ID" });
            DropIndex("dbo.ServiceEligibility", new[] { "Patron_ID" });
            DropIndex("dbo.Patron", new[] { "Ethnicity_ID" });
            DropIndex("dbo.EmergencyContact", new[] { "Patron_ID" });
            DropTable("dbo.Visit");
            DropTable("dbo.Volunteer");
            DropTable("dbo.RoleVolunteer");
            DropTable("dbo.Role");
            DropTable("dbo.ResidenceStatus");
            DropTable("dbo.Race");
            DropTable("dbo.ServiceType");
            DropTable("dbo.ServiceEligibility");
            DropTable("dbo.Patron");
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.Gender");
            DropTable("dbo.Ethnicity");
            DropTable("dbo.EmergencyContact");
        }
    }
}
