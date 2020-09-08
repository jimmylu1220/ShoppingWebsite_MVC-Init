using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingWebsite_MVC.Models
{
    public class ProductComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int productId { get; set; }

        [ForeignKey("productId")]
        public virtual Product product { get; set; }
    }
}