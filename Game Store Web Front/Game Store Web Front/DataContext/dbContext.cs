using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Game_Store_Web_Front.Models;

namespace Game_Store_Web_Front.DataContext
{
    public class dbContext : DbContext
    {
        public dbContext() : base("DefaultConnection") { }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<ImageSrc> Images { get; set; }

        public System.Data.Entity.DbSet<Game_Store_Web_Front.Models.GetSalesDTO> GetSalesDTOes { get; set; }

        public System.Data.Entity.DbSet<Game_Store_Web_Front.Models.GetCartDTO> GetCartDTOes { get; set; }
    }
}
