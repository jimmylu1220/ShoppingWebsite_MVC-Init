using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using ShoppingWebsite_MVC.Models;

namespace ShoppingWebsite_MVC.Controllers.Users
{
    public class UserDatasController : Controller
    {
        private UserDataContext db = new UserDataContext();

        //會員管理後台
        // GET: UserDatas
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //使用者註冊
        public ActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,UserAccount,Password,UserName,Email,Phone")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(userData);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            ModelState.Clear();
            ViewBag.RegisterMessage = userData.UserAccount + "成功註冊!"; 
            return View(userData);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserData user)
        {
            using(UserDataContext db = new UserDataContext())
            {          
                var loginuser = db.Users.SingleOrDefault(u => u.UserAccount == user.UserAccount && u.Password == user.Password);
                if(loginuser != null)
                {
                    Session["UserId"] = loginuser.Id.ToString();
                    Session["UserName"] = loginuser.UserName;
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("","使用者帳號或密碼錯誤!");
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            if(Session["UserId"] != null)
            {
                Session["UserId"] = null;
                Session["UserName"] = null;
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                return View();
            }
        }

        public ActionResult UserAccountCenter(UserData user)
        {
            if (Session["UserId"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userdata = db.Users.Find(Convert.ToInt32(Session["UserId"]));          
            return View(userdata);
        }

        /* GET: UserDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = db.Users.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // GET: UserDatas/Create
        public ActionResult Create() //註冊 
        {
            return View();
        }

        // POST: UserDatas/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserAccount,Password,Email,Phone")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(userData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userData);
        }*/

        // GET: UserDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = db.Users.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }


        // POST: UserDatas/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserData userData)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(userData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(userData);
        }

        // GET: UserDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserData userData = db.Users.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // POST: UserDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserData userData = db.Users.Find(id);
            db.Users.Remove(userData);
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
