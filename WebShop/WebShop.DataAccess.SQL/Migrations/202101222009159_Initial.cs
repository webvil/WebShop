namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductOnSales",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Start = c.DateTimeOffset(nullable: false, precision: 7),
                        End = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.ProductCategories", "ParentId", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "ProductCategoryId", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Brand", c => c.String());
            CreateIndex("dbo.ProductCategories", "ParentId");
            CreateIndex("dbo.Products", "ProductCategoryId");
            AddForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories", "Id");
            AddForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories", "Id");
            DropColumn("dbo.Products", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Category", c => c.String());
            DropForeignKey("dbo.ProductOnSales", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories");
            DropIndex("dbo.ProductOnSales", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.ProductCategories", new[] { "ParentId" });
            DropColumn("dbo.Products", "Brand");
            DropColumn("dbo.Products", "ProductCategoryId");
            DropColumn("dbo.ProductCategories", "ParentId");
            DropTable("dbo.ProductOnSales");
        }
    }
}
