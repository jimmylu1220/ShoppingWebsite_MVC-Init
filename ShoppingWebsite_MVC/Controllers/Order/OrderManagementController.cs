using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingWebsite_MVC.Models;

namespace ShoppingWebsite_MVC.Controllers
{
    public class OrderManagementController : Controller
    {
        private OrderContext db = new OrderContext();
        //訂單後臺管理Controller
        // GET: OrderManagement

        //列出所有訂單
        public ActionResult Index()
        {
            using( Models.OrderContext db = new Models.OrderContext())
            {
                var result = (from s in db.Orders select s).ToList();
                return View(result);
            }             
        }

        // GET: OrderManagement/Details/5
        public ActionResult Details(int id)
        {
            using (Models.OrderContext db = new OrderContext())
            {
                var result = (from s in db.OrderDetails
                              where s.OrderId == id
                              select s).ToList();

                if(result.Count == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }          
        }

        //根據使用者姓名找訂單
        public ActionResult SearchByUserName(string UserName)
        {
            string searchUserName;
            using(Models.UserDataContext db = new UserDataContext())
            {
                searchUserName = (from s in db.Users
                                where s.UserName == UserName
                                select s.UserName).FirstOrDefault();

            }

            if (!String.IsNullOrEmpty(searchUserName))
            {
                using(Models.OrderContext db = new OrderContext())
                {
                    var result = (from s in db.Orders
                                  where s.User.UserName == searchUserName
                                  select s).ToList();

                    return View("Index", result);
                }               
            }
            else
            {
                return View("View", new List<Models.Order>());
            }
        }

        // GET: OrderManagement/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserDatas, "Id", "UserAccount");
            return View();
        }

        // POST: OrderManagement/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,RecieverName,RecieverPhone,RecieverAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserDatas, "Id", "UserAccount", order.UserId);
            return View(order);
        }

        // GET: OrderManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserDatas, "Id", "UserAccount", order.UserId);
            return View(order);
        }

        // POST: OrderManagement/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RecieverName,RecieverPhone,RecieverAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserDatas, "Id", "UserAccount", order.UserId);
            return View(order);
        }

        // GET: OrderManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: OrderManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
