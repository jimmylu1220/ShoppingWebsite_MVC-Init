using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWebsite_MVC.Controllers.ShoppingCart
{
    public class CartController : Controller
    {
        //購物車的Controller
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCart() 
        {
            return PartialView("_CartPartial");
        }

        //把商品加入購物車 以商品Id做識別
        public ActionResult AddtoCart(int id) 
        {
            var currentCart = Models.Operation.GetCurrentCart();
            //呼叫當前購物車加入商品的方法
            currentCart.AddProduct(id);
            return PartialView("_CartPartial");
        }

        //把商品從購物車移除
        public ActionResult RemoveFromCart(int id)
        {
            var currentCart = Models.Operation.GetCurrentCart();
            //呼叫移除購物車商品的方法
            currentCart.RemoveProduct(id);
            return PartialView("_CartPartial");
        }

        //清空購物車
        public ActionResult ClearCart()
        {
            var currentCart = Models.Operation.GetCurrentCart();
            //呼叫清空購物車的方法
            currentCart.ClearCart();
            return PartialView("_CartPartial");
        }
    }
}