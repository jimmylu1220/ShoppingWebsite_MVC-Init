using Antlr.Runtime.Tree;
using ShoppingWebsite_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWebsite_MVC.Controllers
{
    public class HomeController : Controller
    {
        private ShoppingCartDataModel db = new ShoppingCartDataModel();
        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //使用者留言
        [HttpPost]
        public ActionResult AddComment(int id, string Content)
        {
            if(Session["UserId"] != null)
            {
                var userId = Convert.ToInt32(Session["UserId"]);
                var userName = Session["UserName"].ToString();
                var currentDateTime = DateTime.Now;

                var comment = new Models.ProductComment()
                {
                    productId = id,
                    UserName = userName,
                    Content = Content,
                    UserId = userId,
                    CreateTime = currentDateTime
                };

                using (Models.ShoppingCartDataModel db = new ShoppingCartDataModel())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Products", new { id = id });
            }
            else
            {
                return RedirectToAction("Details", "Products", new { id = id });
            }

        }
    }
}