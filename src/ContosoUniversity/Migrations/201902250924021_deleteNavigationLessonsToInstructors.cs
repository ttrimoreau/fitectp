namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteNavigationLessonsToInstructors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lessons", "InstructorID", "dbo.Person");
            DropIndex("dbo.Lessons", new[] { "InstructorID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Lessons", "InstructorID");
            AddForeignKey("dbo.Lessons", "InstructorID", "dbo.Person", "ID", cascadeDelete: true);
        }
    }
}
