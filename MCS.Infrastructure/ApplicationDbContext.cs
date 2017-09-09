// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using MCS.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OpenIddict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> //, ApplicationUserLogin> //, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim> //IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public virtual DbSet<TP_Color_Severity> TP_Color_Severity { get; set; }
        public virtual DbSet<TP_Device> TP_Device { get; set; }
        public virtual DbSet<TP_DeviceStatus> TP_DeviceStatus { get; set; }
        public virtual DbSet<TP_DeviceType> TP_DeviceType { get; set; }
        public virtual DbSet<TP_DeviceTypeXStatus> TP_DeviceTypeXStatus { get; set; }
        public virtual DbSet<TP_Event_State> TP_Event_State { get; set; }
        public virtual DbSet<TP_Events> TP_Events { get; set; }
        public virtual DbSet<TP_Station> TP_Station { get; set; }
        public virtual DbSet<TX_Event_History> TX_Event_History { get; set; }
        public virtual DbSet<TM_Event_History_Dtls> TM_Event_History_Dtls { get; set; }
        public virtual DbSet<TM_DeviceInfo> TM_DeviceInfo { get; set; }
        public virtual DbSet<TM_SystemMap_Dtls> TM_SystemMap_Dtls { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserClaim>().HasOne(pt => pt.ApplicationUser).WithMany(t => t.Claims).HasForeignKey(pt => pt.UserId);
            builder.Entity<ApplicationUserRole>().HasOne(pt => pt.ApplicationUser).WithMany(t => t.Roles).HasForeignKey(pt => pt.UserId);
            builder.Entity<ApplicationUserLogin>().HasOne(pt => pt.ApplicationUser).WithMany(t => t.Logins).HasForeignKey(pt => pt.UserId);



            //builder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            //builder.Entity<Customer>().HasIndex(c => c.Name);
            //builder.Entity<Customer>().Property(c => c.Email).HasMaxLength(100);
            //builder.Entity<Customer>().Property(c => c.PhoneNumber).IsUnicode(false).HasMaxLength(30);
            //builder.Entity<Customer>().Property(c => c.City).HasMaxLength(50);
            //builder.Entity<Customer>().ToTable($"App{nameof(this.Customers)}");

            //builder.Entity<ProductCategory>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            //builder.Entity<ProductCategory>().Property(p => p.Description).HasMaxLength(500);
            //builder.Entity<ProductCategory>().ToTable($"App{nameof(this.ProductCategories)}");

            //builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            //builder.Entity<Product>().HasIndex(p => p.Name);
            //builder.Entity<Product>().Property(p => p.Description).HasMaxLength(500);
            //builder.Entity<Product>().Property(p => p.Icon).IsUnicode(false).HasMaxLength(256);
            //builder.Entity<Product>().HasOne(p => p.Parent).WithMany(p => p.Children).OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<Product>().ToTable($"App{nameof(this.Products)}");

            //builder.Entity<Order>().Property(o => o.Comments).HasMaxLength(500);
            //builder.Entity<Order>().ToTable($"App{nameof(this.Orders)}");

            //builder.Entity<OrderDetail>().ToTable($"App{nameof(this.OrderDetails)}");
        }
    }
}
