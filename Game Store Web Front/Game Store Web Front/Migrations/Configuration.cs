namespace Game_Store_Web_Front.Migrations
{
    using Game_Store_Web_Front.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Game_Store_Web_Front.DataContext.dbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Game_Store_Web_Front.DataContext.dbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Images.AddOrUpdate(
                i => i.imageSource, 
                new ImageSrc { imageSource = "http://i.imgur.com/w1QGGnG.png" },
                new ImageSrc { imageSource = "http://i.imgur.com/fIJTiI3.png" },
                new ImageSrc { imageSource = "http://i.imgur.com/C7D3oW6.png" }
            
            );
        }
    }
}
