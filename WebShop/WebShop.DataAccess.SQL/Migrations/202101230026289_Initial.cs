namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BasketId = c.String(maxLength: 128),
                        ProductId = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketId)
                .Index(t => t.BasketId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Category = c.String(),
                        ParentId = c.String(maxLength: 128),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 20),
                        Description = c.String(),
                        ProductCategoryId = c.String(maxLength: 128),
                        Brand = c.String(),
                        Image = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.ProductsOnSale",
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
            
            CreateTable(
                "dbo.ShoppingCartItems",
                c => new
                    {
                        ShoppingCartItemId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ShoppingCartId = c.String(),
                        Product_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ShoppingCartItemId)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCartItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductsOnSale", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories");
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.ShoppingCartItems", new[] { "Product_Id" });
            DropIndex("dbo.ProductsOnSale", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.ProductCategories", new[] { "ParentId" });
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropTable("dbo.ShoppingCartItems");
            DropTable("dbo.ProductsOnSale");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
        }
    }
}
