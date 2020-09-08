using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingWebsite_MVC.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("ShoppingCartContext")
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<ShoppingWebsite_MVC.Models.UserData> UserDatas { get; set; }
    }
}