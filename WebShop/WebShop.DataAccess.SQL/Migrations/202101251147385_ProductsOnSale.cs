namespace WebShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsOnSale : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductOnSales", newName: "ProductsOnSale");
            AlterColumn("dbo.ProductsOnSale", "Discount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductsOnSale", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            RenameTable(name: "dbo.ProductsOnSale", newName: "ProductOnSales");
        }
    }
}
