namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ParentCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ParentCategory", c => c.String());
        }
    }
}
