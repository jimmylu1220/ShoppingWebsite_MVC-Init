using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingWebsite_MVC.Models
{
    public class UserDataContext :DbContext
    {
        public UserDataContext() :base("ShoppingCartContext")
        {
            
        }
            public DbSet<UserData> Users { get; set; }
    }
}