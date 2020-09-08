using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWebsite_MVC.Models
{
    //在購物車中的單一商品
    [Serializable]
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string DefaultImageURL { get; set; }
        //數量
        public int Quantity { get; set; }
        //這個商品的總價
        public decimal Amount
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}