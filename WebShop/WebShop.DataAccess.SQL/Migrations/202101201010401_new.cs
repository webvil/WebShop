namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "ParentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProductCategories", "ParentId");
            AddForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories", "Id");
            DropColumn("dbo.ProductCategories", "ParentCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategories", "ParentCategory", c => c.String());
            DropForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories");
            DropIndex("dbo.ProductCategories", new[] { "ParentId" });
            DropColumn("dbo.ProductCategories", "ParentId");
        }
    }
}
