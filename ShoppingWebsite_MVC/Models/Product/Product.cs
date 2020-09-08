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
        
        [Display(Name = "�ӫ~�s��")]       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "�ӫ~�W��")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "�ӫ~�y�z")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "���O�s��")]
        public int CategoryId { get; set; }
        [Display(Name = "���")]
        public decimal Price { get; set; }
        [Display(Name = "�o����")]
        [Column(TypeName = "DateTime2")]
        public DateTime PublishDate { get; set; }
        [Display(Name = "�ӫ~���A")]
        public bool Status { get; set; }
        
        public long? DefaultImageId { get; set; }
        [Display(Name = "�s�q")]
        public int Quantity { get; set; }
        [Display(Name = "�Ϥ����}")]
        public string DefaultImageURL { get; set; }

        public virtual ICollection<ProductComment> Comments { get; set; }
    }
}
