namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductCategoryId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "ProductCategoryId");
            AddForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories", "Id");
            DropColumn("dbo.Products", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Category", c => c.String());
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropColumn("dbo.Products", "ProductCategoryId");
        }
    }
}
