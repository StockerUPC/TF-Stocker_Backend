using Stocker_API.IAM.Domain.Model.Aggregates;
using Stocker_API.Profiles.Domain.Model.Aggregates;
using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Inventory.Domain.Model.Entities;
using Stocker_API.Sales.Domain.Model.Aggregates;
using Stocker_API.Sales.Domain.Model.Entities;
using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Model.Entities;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Stocker_API.Profiles.Domain.Model.Entities;

namespace Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Inventory Context
        
        builder.Entity<Category>().HasKey(c => c.Id);
        builder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(30);
        
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Product>().Property(p => p.Description).HasMaxLength(240);
        builder.Entity<Product>().Property(p => p.PhotoUrl).HasMaxLength(255);
        builder.Entity<Product>().Property(p => p.PurchasePrice).IsRequired();
        builder.Entity<Product>().Property(p => p.SalePrice).IsRequired();
        builder.Entity<Product>().Property(p => p.Stock).IsRequired();
        builder.Entity<Product>().Property(p => p.ExpiryDate).HasConversion(
            v => v.ToDateTime(TimeOnly.MinValue), 
            v => DateOnly.FromDateTime(v)
        ).HasColumnType("DATE");   
        
        // Category Relationships
        builder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId)
            .HasPrincipalKey(t => t.Id);
        
        //Sales Context
        builder.Entity<Client>().HasKey(c => c.Id);
        builder.Entity<Client>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Client>().Property(c => c.Name).IsRequired().HasMaxLength(30);
        builder.Entity<Client>().Property(c => c.Number).IsRequired().HasMaxLength(20);
        builder.Entity<Client>().Property(c => c.Email).IsRequired().HasMaxLength(50);
        
        
        builder.Entity<Sale>().HasKey(p => p.Id);
        builder.Entity<Sale>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Sale>().Property(p => p.TotalAmount).IsRequired();
       
        builder.Entity<SaleDetail>().HasKey(sd=> sd.Id);
        builder.Entity<SaleDetail>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SaleDetail>().Property(p => p.SalePrice).IsRequired();
        builder.Entity<SaleDetail>().Property(p => p.Quantity).IsRequired();
        builder.Entity<SaleDetail>().Property(p => p.Subtotal).IsRequired();
        
       //Client Relationships
       builder.Entity<Client>()
           .HasMany(cl => cl.Sales)
           .WithOne(t => t.Client)
           .HasForeignKey(t => t.ClientId)
           .HasPrincipalKey(t => t.Id);
       
       //Sale Relationships
       builder.Entity<Sale>()
           .HasMany(s=> s.SaleDetails)
              .WithOne(sd => sd.Sale)
                .HasForeignKey(sd => sd.SaleId)
                .HasPrincipalKey(sd => sd.Id);
       
       //Purchases Context
       builder.Entity<Supplier>().HasKey(c => c.Id);
       builder.Entity<Supplier>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
       builder.Entity<Supplier>().Property(c => c.Name).IsRequired().HasMaxLength(30);
       builder.Entity<Supplier>().Property(c => c.Number).IsRequired().HasMaxLength(20);
       builder.Entity<Supplier>().Property(c => c.Email).IsRequired().HasMaxLength(50);
        
        
       builder.Entity<Purchase>().HasKey(p => p.Id);
       builder.Entity<Purchase>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
       builder.Entity<Purchase>().Property(p => p.TotalAmount).IsRequired();
       
       builder.Entity<PurchaseDetail>().HasKey(sd=> sd.Id);
       builder.Entity<PurchaseDetail>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
       builder.Entity<PurchaseDetail>().Property(p => p.PurchasePrice).IsRequired();
       builder.Entity<PurchaseDetail>().Property(p => p.SalePrice).IsRequired();
       builder.Entity<PurchaseDetail>().Property(p => p.Quantity).IsRequired();
       builder.Entity<PurchaseDetail>().Property(p => p.Total).IsRequired();
        
       //Supplier Relationships
       builder.Entity<Supplier>()
           .HasMany(sp => sp.Purchases)
           .WithOne(p => p.Supplier)
           .HasForeignKey(p => p.SupplierId)
           .HasPrincipalKey(s => s.Id);
       
       //Purchase Relationships
       builder.Entity<Purchase>()
           .HasMany(s=> s.PurchaseDetails)
           .WithOne(sd => sd.Purchase)
           .HasForeignKey(pd => pd.PurchaseId)
           .HasPrincipalKey(pd => pd.Id);
       
       //Product Relationships
       builder.Entity<Product>()
           .HasMany(p=> p.SaleDetails)
           .WithOne(sd => sd.Product)
           .HasForeignKey(sd => sd.ProductId)
           .HasPrincipalKey(sd => sd.Id);
       
        // Profiles Context
        builder.Entity<Subscription>().HasKey(c => c.Id);
        builder.Entity<Subscription>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Subscription>().Property(c => c.Name).IsRequired().HasMaxLength(30);
        builder.Entity<Subscription>().Property(c => c.MonthlyPrice).IsRequired();
        
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().OwnsOne(p => p.Name,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.FirstName).HasColumnName("FirstName");
                n.Property(p => p.LastName).HasColumnName("LastName");
            });

        builder.Entity<Profile>().OwnsOne(p => p.Email,
            e =>
            {
                e.WithOwner().HasForeignKey("Id");
                e.Property(a => a.Address).HasColumnName("EmailAddress");
            });

        builder.Entity<Profile>().OwnsOne(p => p.Address,
            a =>
            {
                a.WithOwner().HasForeignKey("Id");
                a.Property(s => s.Street).HasColumnName("AddressStreet");
                a.Property(s => s.Number).HasColumnName("AddressNumber");
                a.Property(s => s.City).HasColumnName("AddressCity");
                a.Property(s => s.PostalCode).HasColumnName("AddressPostalCode");
                a.Property(s => s.Country).HasColumnName("AddressCountry");
            });
        
        // Subscription Relationships
        builder.Entity<Subscription>()
            .HasMany(s => s.Profiles)
            .WithOne(p => p.Subscription)
            .HasForeignKey(p =>p.SubscriptionId)
            .HasPrincipalKey(s =>s.Id);
        
        // IAM Context
        
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}