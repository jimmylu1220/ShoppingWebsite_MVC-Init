namespace ShoppingWebsite_MVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShoppingCartDataModel : DbContext
    {
        public ShoppingCartDataModel()
            : base("name=ShoppingCartContext")
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductComment> Comments { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(28, 3);
        }
    }
}
