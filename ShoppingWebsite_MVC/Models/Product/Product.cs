namespace ShoppingWebsite_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        
        [Display(Name = "商品編號")]       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "商品名稱")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "商品描述")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "類別編號")]
        public int CategoryId { get; set; }
        [Display(Name = "售價")]
        public decimal Price { get; set; }
        [Display(Name = "發行日期")]
        [Column(TypeName = "DateTime2")]
        public DateTime PublishDate { get; set; }
        [Display(Name = "商品狀態")]
        public bool Status { get; set; }
        
        public long? DefaultImageId { get; set; }
        [Display(Name = "存量")]
        public int Quantity { get; set; }
        [Display(Name = "圖片網址")]
        public string DefaultImageURL { get; set; }

        public virtual ICollection<ProductComment> Comments { get; set; }
    }
}
