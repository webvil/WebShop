namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductOnSales", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductOnSales", new[] { "ProductId" });
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 20),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Message = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ProductOnSales");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Contacts");
            CreateIndex("dbo.ProductOnSales", "ProductId");
            AddForeignKey("dbo.ProductOnSales", "ProductId", "dbo.Products", "Id");
        }
    }
}
