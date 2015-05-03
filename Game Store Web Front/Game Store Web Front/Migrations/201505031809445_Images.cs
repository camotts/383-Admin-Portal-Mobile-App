namespace Game_Store_Web_Front.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageSrcs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        imageSource = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageSrcs");
        }
    }
}
