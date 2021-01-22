﻿namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductsOnSale : DbMigration
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
            
            
            
        }
        
        public override void Down()
        {
           
            DropForeignKey("dbo.ProductOnSales", "ProductId", "dbo.Products");
           
        }
    }
}
