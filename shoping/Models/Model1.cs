namespace shoping.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact_us> Contact_us { get; set; }
        public virtual DbSet<Feedbook> Feedbooks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Order_details> Order_details { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sub_category> Sub_category { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Feedbooks)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.Account_fid);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.Account_Fid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Sub_category)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.Category_Fid);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_details)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.Order_Fid);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Feedbooks)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.Product_fid);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_details)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Product_Fid);

            modelBuilder.Entity<Sub_category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Sub_category)
                .HasForeignKey(e => e.Sub_Category_Fid);
        }
    }
}
