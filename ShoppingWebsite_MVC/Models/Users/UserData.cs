using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingWebsite_MVC.Models
{
    public class UserData
    {
        [Display(Name = "會員編號")]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入會員帳號")]
        [Display(Name = "會員帳號")]
        public string UserAccount { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "請輸入姓名")]
        [Display(Name = "姓名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "請輸入信箱")]
        [Display(Name = "信箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入電話")]
        [Display(Name = "電話")]
        public string Phone { get; set; } 

        //導覽屬性 一個使用者可能有多筆訂單
        public virtual ICollection<Order> Orders { get; set; }
    }
}