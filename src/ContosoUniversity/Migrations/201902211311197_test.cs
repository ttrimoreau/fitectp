namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Person", name: "FirstName", newName: "FirstMidName");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Person", name: "FirstMidName", newName: "FirstName");
        }
    }
}
