namespace SrProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Context1Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthenticationToken",
                c => new
                    {
                        Token = c.Guid(nullable: false),
                        LastAccessedTime = c.DateTime(nullable: false),
                        AssociatedVolunteer_Username = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Token)
                .ForeignKey("dbo.Volunteer", t => t.AssociatedVolunteer_Username, cascadeDelete: true)
                .Index(t => t.AssociatedVolunteer_Username);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthenticationToken", "AssociatedVolunteer_Username", "dbo.Volunteer");
            DropIndex("dbo.Volunteer", new[] { "Username" });
            DropIndex("dbo.AuthenticationToken", new[] { "AssociatedVolunteer_Username" });
            DropTable("dbo.Volunteer");
            DropTable("dbo.AuthenticationToken");
        }
    }
}
