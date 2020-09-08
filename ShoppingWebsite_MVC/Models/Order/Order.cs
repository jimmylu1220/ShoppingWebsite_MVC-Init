using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingWebsite_MVC.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        
        //Foreign Key欄位
        public int UserId { get; set; }
        public string RecieverName { get; set; }
        public string RecieverPhone { get; set; }
        public string RecieverAddress { get; set; }

        [ForeignKey("UserId")]
        public virtual UserData User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}