namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNavigationLessonsToInstructors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "InstructorID_ID", c => c.Int());
            CreateIndex("dbo.Lessons", "InstructorID_ID");
            AddForeignKey("dbo.Lessons", "InstructorID_ID", "dbo.Person", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "InstructorID_ID", "dbo.Person");
            DropIndex("dbo.Lessons", new[] { "InstructorID_ID" });
            DropColumn("dbo.Lessons", "InstructorID_ID");
        }
    }
}
