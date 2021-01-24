namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyProduct_ProductOnSale : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductsOnSale", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductsOnSale", new[] { "ProductId" });
            AlterColumn("dbo.ProductsOnSale", "ProductId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ProductsOnSale", "ProductId");
            AddForeignKey("dbo.ProductsOnSale", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsOnSale", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductsOnSale", new[] { "ProductId" });
            AlterColumn("dbo.ProductsOnSale", "ProductId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProductsOnSale", "ProductId");
            AddForeignKey("dbo.ProductsOnSale", "ProductId", "dbo.Products", "Id");
        }
    }
}
