using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ShoppingWebsite_MVC.Models
{
    //對購物車的操作
    public class Operation
    {
        [WebMethod(EnableSession = true)]
        public static Models.Cart GetCurrentCart()
        {
            if(System.Web.HttpContext.Current != null) //購物車不存在則新增空的CART
            {
                if (System.Web.HttpContext.Current.Session["Cart"] == null) 
                {
                    var order = new Cart();
                    System.Web.HttpContext.Current.Session["Cart"] = order;
                }

                return (Cart)System.Web.HttpContext.Current.Session["Cart"];
            }
            else 
            {
                throw new InvalidOperationException("System.Web.HttpContext.Current為空, 請檢查");
            }
        }

    }
}