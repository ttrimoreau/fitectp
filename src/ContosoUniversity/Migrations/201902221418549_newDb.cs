namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lessons", "Course_CourseID", "dbo.Course");
            DropIndex("dbo.Lessons", new[] { "Course_CourseID" });
            RenameColumn(table: "dbo.Person", name: "FirstName", newName: "FirstMidName");
            RenameColumn(table: "dbo.Lessons", name: "Course_CourseID", newName: "CourseID");
            AlterColumn("dbo.FileImage", "ContentType", c => c.String(maxLength: 100));
            AlterColumn("dbo.Lessons", "CourseID", c => c.Int(nullable: false));
            CreateIndex("dbo.Lessons", "CourseID");
            AddForeignKey("dbo.Lessons", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "CourseID", "dbo.Course");
            DropIndex("dbo.Lessons", new[] { "CourseID" });
            AlterColumn("dbo.Lessons", "CourseID", c => c.Int());
            AlterColumn("dbo.FileImage", "ContentType", c => c.String());
            RenameColumn(table: "dbo.Lessons", name: "CourseID", newName: "Course_CourseID");
            RenameColumn(table: "dbo.Person", name: "FirstMidName", newName: "FirstName");
            CreateIndex("dbo.Lessons", "Course_CourseID");
            AddForeignKey("dbo.Lessons", "Course_CourseID", "dbo.Course", "CourseID");
        }
    }
}
