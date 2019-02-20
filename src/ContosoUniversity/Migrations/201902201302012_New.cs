namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileImage", "ContentType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileImage", "ContentType", c => c.String(maxLength: 100));
        }
    }
}
