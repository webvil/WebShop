namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParentCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "ParentCategory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "ParentCategory");
        }
    }
}
