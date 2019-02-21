namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedboLessonsDateTime1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lessons", "HourStart", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lessons", "HourStart", c => c.Time(nullable: false, precision: 7));
        }
    }
}
