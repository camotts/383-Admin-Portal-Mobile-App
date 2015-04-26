namespace Game_Store_Web_Front.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIDAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CardNum = c.String(),
                        Expiration = c.DateTime(nullable: false),
                        Ammount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payments");
        }
    }
}
