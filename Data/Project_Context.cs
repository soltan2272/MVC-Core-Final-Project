using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Configration;
using Models.Models;

using Models.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class Project_Context : IdentityDbContext<User,Role,int>
    {

        public Project_Context(DbContextOptions<Project_Context> options)
            : base(options)
        {
        }

        
     //   public DbSet<Supplier> Suplliers { get; set; }
      //  public DbSet<Store> Stores { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductFeedback> ProductFeedbacks { get; set; }
     //   public DbSet<StoreProduct> storeProducts { get; set; }
        public DbSet<ProductOffer> productOffers { get; set; }
      //  public DbSet<SupplierStore> SupplierStores { get; set; }
       // public DbSet<Admin> Admins { get; set; }
       // public DbSet<Contact> Contacts { get; set; }
       
        public DbSet<AdminProduct> AdminProducts { get; set; }

        //  public DbSet<AdminStore> AdminStores { get; set; }

        public DbSet<Images> Images { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            //modelBuilder.ApplyConfiguration(new StoreEntityConfiguration());
          //  modelBuilder.ApplyConfiguration(new SupplierEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CourierEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentEnityConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbackEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ImageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new prodFeedbactConfig());
            modelBuilder.ApplyConfiguration(new ProdOfferConfig());
            modelBuilder.ApplyConfiguration(new ProdOrderConfig());
            //  modelBuilder.ApplyConfiguration(new AdminEntityConfiguration());
            // modelBuilder.ApplyConfiguration(new ContactEntityConfiguration());

            /*  modelBuilder.Entity<SupplierStore>().HasKey(ss => new { ss.Store_ID, ss.Supllier_ID });
              modelBuilder.Entity<SupplierStore>()
              .HasOne<User>(s => s.supplier)
              .WithMany(sp => sp.SupllierStores)
              .HasForeignKey(sp => sp.Supllier_ID);

              modelBuilder.Entity<SupplierStore>()
              .HasOne<Store>(s => s.store)
              .WithMany(sp => sp.SupllierStores)
              .HasForeignKey(sp => sp.Store_ID);


              modelBuilder.Entity<StoreProduct>().HasKey(sp => new { sp.Store_ID, sp.Product_ID});
              modelBuilder.Entity<StoreProduct>()
              .HasOne<Product>(p => p.product)
              .WithMany(sp => sp.StoresProducts)
              .HasForeignKey(sp => sp.Product_ID);

              modelBuilder.Entity<StoreProduct>()
              .HasOne<Store>(s => s.store)
              .WithMany(sp => sp.StoresProducts)
              .HasForeignKey(sp => sp.Store_ID);*/

            modelBuilder.Entity<ProductOffer>().HasKey(po => new { po.Offer_ID, po.Product_ID });
            modelBuilder.Entity<ProductOffer>()
            .HasOne<Product>(p => p.product)
            .WithMany(po => po.ProductOffers)
            .HasForeignKey(pf => pf.Product_ID);

            modelBuilder.Entity<ProductOffer>()
            .HasOne<Offer>(f => f.offer)
            .WithMany(po => po.ProductOffers)
            .HasForeignKey(pf => pf.Offer_ID);

            modelBuilder.Entity<ProductFeedback>().HasKey(pf => new { pf.Feedback_ID, pf.Product_ID });
            modelBuilder.Entity<ProductFeedback>()
            .HasOne<Product>(p => p.Product)
            .WithMany(pf => pf.productFeedbacks)
            .HasForeignKey(pf => pf.Product_ID)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductFeedback>()
            .HasOne<Feedback>(f => f.Feedback)
            .WithMany(pf => pf.productFeedbacks)
            .HasForeignKey(pf => pf.Feedback_ID)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductOrder>().HasKey(pf => new { pf.Order_ID, pf.Product_ID });
            modelBuilder.Entity<ProductOrder>()
            .HasOne<Product>(p => p.product)
            .WithMany(pf => pf.productOrders)
            .HasForeignKey(pf => pf.Product_ID)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductOrder>()
            .HasOne<Order>(f => f.Order)
            .WithMany(pf => pf.productOrders)
            .HasForeignKey(pf => pf.Order_ID)
             .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<AdminProduct>().HasKey(Ap => new { Ap.Product_ID, Ap.Admin_ID });
              
            modelBuilder.Entity<AdminProduct>()
            .HasOne<User>(a => a.Admin)
            .WithMany(Ap => Ap.AdminProducts)
            .HasForeignKey(a => a.Admin_ID)
            .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<AdminProduct>()
            .HasOne<Product>(p => p.Product)
            .WithMany(Ap => Ap.AdminProducts)
            .HasForeignKey(p => p.Product_ID)
            .OnDelete(DeleteBehavior.Restrict);

       /*     modelBuilder.Entity<AdminStore>().HasKey(As => new { As.Store_ID, As.Admin_ID });
            modelBuilder.Entity<AdminStore>()
            .HasOne<User>(a => a.Admin)
            .WithMany(As => As.AdminStores)
            .HasForeignKey(a => a.Admin_ID);

            modelBuilder.Entity<AdminStore>()
            .HasOne<Store>(s => s.Store)
            .WithMany(As => As.AdminStores)
            .HasForeignKey(a => a.Store_ID);*/
           



            modelBuilder.Entity<Product>()
            .HasOne<User>(s => s.supplier)
            .WithMany(g => g.Products)
            .HasForeignKey(s => s.CurrentSupplierID);

            modelBuilder.Entity<Product>()
             .HasOne<Category>(s => s.category)
             .WithMany(p => p.Products)
             .HasForeignKey(s => s.CurrentCategoryID);

            //modelBuilder.Entity<Order>()
            //.HasOne<Courier>(c => c.Courier)
            //.WithMany(o => o.Orders)
            //.HasForeignKey(c => c.CurrentCourierID);

            //modelBuilder.Entity<Order>()
            //.HasOne<Payment>(p => p.Payment)
            //.WithMany(o => o.Orders)
            //.HasForeignKey(p => p.CurrentPaymentID);

            modelBuilder.Entity<Order>()
            .HasOne<User>(u => u.User)
            .WithMany(o => o.Orders)
            .HasForeignKey(u => u.CurrentUserID);

            modelBuilder.Entity<Feedback>()
            .HasOne<User>(u => u.User)
            .WithMany(f => f.Feedbacks)
            .HasForeignKey(u => u.CurrentUserID);

          modelBuilder.Entity<Images>()
         .HasOne<Product>(u => u.product)
         .WithMany(f => f.Product_Images)
         .HasForeignKey(u => u.CurrentProductID);

            /*   modelBuilder.Entity<User>()
               .HasOne<Contact>(c=>c.Contacts)
               .WithMany(a => a.Admins)
               .HasForeignKey(a => a.CurrentContactID)
               .OnDelete(DeleteBehavior.Restrict);*/

            base.OnModelCreating(modelBuilder);
        }
    }
}
