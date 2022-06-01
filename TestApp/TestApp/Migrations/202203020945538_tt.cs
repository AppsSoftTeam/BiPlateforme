namespace TestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Dashboards", "CreationDate");
            DropColumn("dbo.Reports", "CreationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Dashboards", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}
