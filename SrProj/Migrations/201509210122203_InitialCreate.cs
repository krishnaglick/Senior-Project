namespace SrProj.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        ModifyDate = c.DateTime(nullable: false),
                        Patron_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Patron", t => t.Patron_ID)
                .Index(t => t.Patron_ID);
            
            CreateTable(
                "dbo.Patron",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmergencyContact", "Patron_ID", "dbo.Patron");
            DropIndex("dbo.EmergencyContact", new[] { "Patron_ID" });
            DropTable("dbo.Patron");
            DropTable("dbo.EmergencyContact");
        }
    }
}
