using SupplyManagement_NET48.Models;
using System.Data.Entity;

namespace SupplyManagement_NET48.Data
{
    public class SupplyManagementDbContext : DbContext
    {
        public SupplyManagementDbContext() : base("DefaultConnection") { }

        // Tables
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<AccountVendor> AccountVendors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Constraints Unique
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Vendor>()
                .HasIndex(v => new { v.Email, v.PhoneNumber })
                .IsUnique();

            // Account - AccountRole (One to Many)
            modelBuilder.Entity<Account>()
                .HasMany(account => account.AccountRoles)
                .WithRequired(accountRole => accountRole.Account)
                .HasForeignKey(accountRole => accountRole.AccountGuid);

            // Account - Employee (One to One)
            modelBuilder.Entity<Account>()
                .HasRequired(account => account.Employee)
                .WithOptional(employee => employee.Account);

            // AccountVendor - Vendor (One to One)
            modelBuilder.Entity<AccountVendor>()
                .HasRequired(accountVendor => accountVendor.Vendor)
                .WithOptional(vendor => vendor.AccountVendor);

            // Role - AccountRole (One to Many)
            modelBuilder.Entity<Role>()
                .HasMany(role => role.AccountRoles)
                .WithRequired(accountRole => accountRole.Role)
                .HasForeignKey(accountRole => accountRole.RoleGuid);

            // Vendor - Project (One to Many)
            modelBuilder.Entity<Vendor>()
                .HasMany(vendor => vendor.Projects)
                .WithRequired(project => project.Vendor)
                .HasForeignKey(project => project.VendorGuid);
        }
    }
}