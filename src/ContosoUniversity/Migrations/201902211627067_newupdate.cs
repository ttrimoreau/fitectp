namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lessons", "CourseID", "dbo.Course");
            DropIndex("dbo.Lessons", new[] { "CourseID" });
            RenameColumn(table: "dbo.Lessons", name: "CourseID", newName: "Course_CourseID");
            AlterColumn("dbo.Lessons", "Course_CourseID", c => c.Int());
            CreateIndex("dbo.Lessons", "Course_CourseID");
            AddForeignKey("dbo.Lessons", "Course_CourseID", "dbo.Course", "CourseID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "Course_CourseID", "dbo.Course");
            DropIndex("dbo.Lessons", new[] { "Course_CourseID" });
            AlterColumn("dbo.Lessons", "Course_CourseID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Lessons", name: "Course_CourseID", newName: "CourseID");
            CreateIndex("dbo.Lessons", "CourseID");
            AddForeignKey("dbo.Lessons", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
    }
}
