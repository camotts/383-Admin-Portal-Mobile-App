namespace Game_Store_Web_Front.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _this : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GetSalesDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        SalesDate = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GetCartDTOes", t => t.Cart_Id)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.GetCartDTOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        CheckoutReady = c.Boolean(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GetSalesDTOes", "Cart_Id", "dbo.GetCartDTOes");
            DropIndex("dbo.GetSalesDTOes", new[] { "Cart_Id" });
            DropTable("dbo.GetCartDTOes");
            DropTable("dbo.GetSalesDTOes");
        }
    }
}
