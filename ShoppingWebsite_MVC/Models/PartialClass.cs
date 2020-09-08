using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWebsite_MVC.Models
{
    public class PartialClass
    {
    }

    public partial class Order
    {
        public string GetUserName()
        {
            using (Models.OrderContext db = new OrderContext())
            {
                var result = (from s in db.UserDatas
                              where s.Id == this.UserId
                              select s.UserName).FirstOrDefault();

                return result;
            }
        }
    }
    
}