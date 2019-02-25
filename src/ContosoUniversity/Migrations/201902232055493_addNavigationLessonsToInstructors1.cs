namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNavigationLessonsToInstructors1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lessons", "InstructorID_ID", "dbo.Person");
            DropIndex("dbo.Lessons", new[] { "InstructorID_ID" });
            RenameColumn(table: "dbo.Lessons", name: "InstructorID_ID", newName: "InstructorID");
            AlterColumn("dbo.Lessons", "InstructorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Lessons", "InstructorID");
            AddForeignKey("dbo.Lessons", "InstructorID", "dbo.Person", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "InstructorID", "dbo.Person");
            DropIndex("dbo.Lessons", new[] { "InstructorID" });
            AlterColumn("dbo.Lessons", "InstructorID", c => c.Int());
            RenameColumn(table: "dbo.Lessons", name: "InstructorID", newName: "InstructorID_ID");
            CreateIndex("dbo.Lessons", "InstructorID_ID");
            AddForeignKey("dbo.Lessons", "InstructorID_ID", "dbo.Person", "ID");
        }
    }
}
