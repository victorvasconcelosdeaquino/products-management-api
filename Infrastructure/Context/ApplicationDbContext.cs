using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasKey(c => c.Id);
            builder.Entity<Supplier>().HasKey(c => c.Id);

            builder.Entity<Product>()
                .HasOne<Supplier>(c => c.Supplier)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.SupplierId);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            string mySqlConnection = configuration.GetConnectionString("Connection");

            optionsBuilder.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection));
        }
    }
}
