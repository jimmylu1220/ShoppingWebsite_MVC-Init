using Microsoft.Ajax.Utilities;
using ShoppingWebsite_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWebsite_MVC.Controllers
{
    public class OrderController : Controller
    {

        
        //結帳頁面的controller
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        //列出當前購物車的所有商品
        [HttpPost]
        public ActionResult Index(Models.ShipInfo postback)
        {
            if (this.ModelState.IsValid)
            {
                TempData["Ship"] = postback;
                return RedirectToAction("Checkout");
            }
            return View();
        }

        //使用者 我的訂單功能
        public ActionResult MyOrder()
        {
            var userId = Convert.ToInt32(Session["UserId"]);

            using(Models.OrderContext db = new Models.OrderContext())
            {
                //從訂單資料表中抓出符合當前使用者Id的訂單
                var result = (from s in db.Orders
                              where s.UserId == userId
                              select s).ToList();

                return View(result);
            }
        }

        public ActionResult MyOrderDetail(int id)
        {
            using(Models.OrderContext db = new Models.OrderContext())
            {
                //從訂單明細資料表中找出符合當前訂單Id的明細
                var result = (from s in db.OrderDetails
                              where s.OrderId == id
                              select s).ToList();

                if(result.Count == 0)
                {
                    return RedirectToAction("MyOrder");
                }
                else
                {
                    return View(result);
                }
            }
        }

        public ActionResult Checkout()
        {
            var Ship = (ShipInfo)TempData["Ship"];
            return View(Ship);
        }

        [HttpPost]
        public ActionResult Checkout(Models.ShipInfo postback)
        {
                
                var currentcart = Models.Operation.GetCurrentCart();

                var userId = Convert.ToInt32(Session["UserId"]);

                using (Models.OrderContext db = new Models.OrderContext())
                {
                    //新增一筆訂單
                    var order = new Models.Order()
                    {
                        UserId = userId,
                        RecieverName = postback.RecieverName,
                        RecieverAddress = postback.RecieverAddress,
                        RecieverPhone = postback.RecieverPhone
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();

                    //將購物車商品加入變成訂單紀錄
                    var orderDetails = currentcart.ToOrderDetailList(order.Id);
                    db.OrderDetails.AddRange(orderDetails);
                    db.SaveChanges();
                }
                currentcart.ClearCart();
                return RedirectToAction("Index", "Home");                       
        }
    }
}