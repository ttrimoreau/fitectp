namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Person", name: "FirstName", newName: "FirstMidName");
            CreateTable(
                "dbo.FileImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileType = c.Int(nullable: false),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        PersonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        HourStart = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            AddColumn("dbo.Course", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Person", "UserName", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Person", "Password", c => c.String(nullable: false, maxLength: 64));
            AddColumn("dbo.Person", "Email", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "CourseID", "dbo.Course");
            DropForeignKey("dbo.FileImage", "PersonID", "dbo.Person");
            DropIndex("dbo.Lessons", new[] { "CourseID" });
            DropIndex("dbo.FileImage", new[] { "PersonID" });
            DropColumn("dbo.Person", "Email");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "UserName");
            DropColumn("dbo.Course", "StartDate");
            DropTable("dbo.Lessons");
            DropTable("dbo.FileImage");
            RenameColumn(table: "dbo.Person", name: "FirstMidName", newName: "FirstName");
        }
    }
}
