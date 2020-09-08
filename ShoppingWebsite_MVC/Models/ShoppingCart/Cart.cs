using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWebsite_MVC.Models
{
    //購物車
    [Serializable]
    public class Cart : IEnumerable<CartItem>
    {
        private List<CartItem> cartItems;
        public Cart()
        {
            this.cartItems = new List<CartItem>(); 
        }
        
        //取得購物車商品總數量
        public int Count
        {
            get 
            {
                return cartItems.Count;
            }
        
        }

        public bool AddProduct(int ProductId) 
        {
            //傳回序列中符合條件的第一個元素；如果找不到這類元素，則傳回預設值。
            var findItem = this.cartItems
                .Where(s => s.Id == ProductId)
                .Select(s => s)
                .FirstOrDefault();

            //判斷相同Id的CartItem是否已存在在購物車內
            if(findItem == default(Models.CartItem)) 
            {
                using(Models.ShoppingCartDataModel db = new ShoppingCartDataModel()) 
                {
                    //從商品資料表裡找出Id符合的商品加入購物車
                    var product = (from s in db.Product
                                   where s.Id == ProductId
                                   select s).FirstOrDefault();
                    if( product != default(Models.Product))  //如果有找到商品
                    {
                        this.AddProduct(product);
                    }
                }
            }
            //已在購物車裡則數量加1
            else 
            {
                findItem.Quantity += 1;
            }
            return true;            
        }

        public bool AddProduct(Product product) 
        {
            var cartItem = new Models.CartItem()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                DefaultImageURL = product.DefaultImageURL,
                Quantity = 1
            };

            this.cartItems.Add(cartItem);
            return true;
        }

        public bool RemoveProduct(int productId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == productId)
                            .Select(s => s)
                            .FirstOrDefault();

            if (findItem == default(Models.CartItem))
            {
                //沒找到商品 不做任何事
            }
            else
            {
                this.cartItems.Remove(findItem);
            }
            return true;
        }

        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }

        //計算總價格
        public decimal TotalAmount
        {
            get 
            {
                decimal totalAmount = 0.0m;
                foreach(var cartItem in this.cartItems)
                {
                    totalAmount += cartItem.Amount;
                }
                return totalAmount;
            }
        }

        //加入訂單紀錄
        public List<Models.OrderDetail> ToOrderDetailList(int orderId)
        {
            var result = new List<Models.OrderDetail>();
            foreach (var cartItem in this.cartItems)
            {
                result.Add(new Models.OrderDetail()
                {
                    Name = cartItem.Name,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    OrderId = orderId
                });
            }
            return result;
        }

        IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }
    }
}