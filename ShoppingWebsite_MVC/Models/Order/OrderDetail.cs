using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWebsite_MVC.Models
{
    public class OrderDetail
    {
        public int Id { get; set; } 

        //Foreign Key欄位
        public int OrderId { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
    }
}