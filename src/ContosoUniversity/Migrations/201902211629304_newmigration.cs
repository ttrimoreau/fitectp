namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileImage", "ContentType", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileImage", "ContentType", c => c.String());
        }
    }
}