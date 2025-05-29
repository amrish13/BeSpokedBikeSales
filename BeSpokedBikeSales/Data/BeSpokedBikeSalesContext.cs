using BeSpokedBikeSales.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BeSpokedBikeSales.Data
{
    public class BeSpokedBikeSalesContext : DbContext
    {
        public BeSpokedBikeSalesContext (DbContextOptions<BeSpokedBikeSalesContext> options)
            : base(options)
        {
        }

        public DbSet<BeSpokedBikeSales.Models.SalesPerson> SalesPerson { get; set; } = default!;
        public DbSet<BeSpokedBikeSales.Models.Product> Product { get; set; } = default!;
        public DbSet<BeSpokedBikeSales.Models.Customer> Customer { get; set; } = default!;
        public DbSet<BeSpokedBikeSales.Models.Sale> Sale { get; set; } = default!;
        public DbSet<BeSpokedBikeSales.Models.Discount> Discount { get; set; } = default!;
        public DbSet<QuaterlyCommissionReport> QuaterlyCommissionReport { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<SalesPerson>().HasData(
                new SalesPerson { SalesPersonId = 1, FirstName = "Sales", LastName = "Person", Address = "123 Elm St", Phone = "555-1234", StartDate = DateTime.Now.AddYears(-1), Manager = "Sales Manager" },
                new SalesPerson { SalesPersonId = 2, FirstName = "Alice", LastName = "Smith", Address = "456 Oak St", Phone = "555-5678", StartDate = DateTime.Now.AddYears(-2), Manager = "Sales Manager" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Mountain Bike", CommisionPercentage = 12, QuantityOnHand = 32, PurchasePrice = (decimal)2.00, SalePrice = (decimal)5.00 },
                new Product { ProductId = 2, Name = "Road Bike", CommisionPercentage = 24, QuantityOnHand = 65, PurchasePrice = (decimal)21.00, SalePrice = (decimal)35.00 }
                );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Phone = "123"},
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Johnson", Phone = "123" }
                );

            modelBuilder.Entity<Discount>().HasData(
                new Discount
                {
                    DiscountId = 1,
                    ProductId = 2,
                    BeginDate = new DateTime(2025, 05, 27),
                    DiscountPercentage = 10,
                    EndDate = new DateTime(2025, 05, 27)
                },
                new Discount { DiscountId = 2, ProductId = 1, DiscountPercentage = 21, BeginDate = new DateTime(2025, 06, 27) }
                );

            modelBuilder.Entity<Sale>().HasData(
                new Sale
                {
                    SaleId = 1,
                    CustomerId = 1,
                    SalesPersonId = 1,
                    Price = (decimal)35.00,
                    ProductId = 1
                },

                new Sale { SaleId = 2, CustomerId = 2, Price=35, ProductId = 2, SalesPersonId = 2}
                );

            modelBuilder.Entity<QuaterlyCommissionReport>().HasNoKey().ToView(null);
        }
    }
}
