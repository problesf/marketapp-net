using System.Globalization;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using MarketApp.src.Domain.entities.order;
using MarketApp.src.Domain.entities.product;
using MarketNet.src.Domain.entities.Customers;
using MarketNet.src.Domain.entities.Inventory;
using MarketNet.src.Domain.entities.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MarketNet.src.Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItem => Set<OrderItem>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Shipment> Shipment => Set<Shipment>();

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            // PRODUCT
            model.Entity<Product>(entity =>
            {
                entity.ToTable("product");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Stock)
                    .IsRequired();

                entity.Property(p => p.Description)
                    .HasMaxLength(1000);

                entity.Property(p => p.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.IsActive)
                    .IsRequired();

                entity.Property(p => p.Currency)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(p => p.TaxRate)
                    .IsRequired()
                    .HasColumnType("decimal(5,2)");

                entity.HasIndex(e => e.Code).IsUnique();

                entity.HasMany(p => p.Categories).WithMany(c => c.Products);
            });

            // CATEGORY
            model.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Slug)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(c => c.Slug).IsUnique();

                entity.HasOne(c => c.ParentCategory)
                      .WithMany(c => c.ChildCategories)
                      .HasForeignKey(c => c.ParentCategoryId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // INVENTORY MOVEMENT
            model.Entity<InventoryMovement>(entity =>
            {
                entity.ToTable("inventory_movements");
                entity.HasKey(im => im.Id);

                entity.Property(im => im.Quantity)
                    .IsRequired();

                entity.Property(im => im.Reason)
                    .IsRequired();

                entity.Property(im => im.Reference)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(im => im.Product)
                    .WithMany(p => p.InventoryMovements)
                    .HasForeignKey(im => im.ProductId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //CUSTOMER
            model.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Email)
                      .IsRequired()
                      .HasMaxLength(255);
                entity.HasIndex(c => c.Email)
                      .IsUnique();

                entity.Property(c => c.FullName)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.HasMany(c => c.Addresses)
                      .WithOne(a => a.Customer)
                      .HasForeignKey(a => a.CustomerId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.DefaultBillingAddress)
                      .WithMany()
                      .HasForeignKey(c => c.DefaultBillingAddressId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(c => c.DefaultShippingAddress)
                      .WithMany()
                      .HasForeignKey(c => c.DefaultShippingAddressId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ADDRESS
            model.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Line1).IsRequired().HasMaxLength(200);
                entity.Property(a => a.Line2).HasMaxLength(200);
                entity.Property(a => a.City).IsRequired().HasMaxLength(120);
                entity.Property(a => a.State).IsRequired().HasMaxLength(120);
                entity.Property(a => a.PostalCode).IsRequired().HasMaxLength(20).IsUnicode(false);
                entity.Property(a => a.Country).IsRequired().HasMaxLength(2).IsUnicode(false);

                entity.Property(a => a.IsDefaultBilling).HasDefaultValue(false);
                entity.Property(a => a.IsDefaultShipping).HasDefaultValue(false);

                entity.HasOne<Customer>()
                      .WithMany(c => c.Addresses)
                      .HasForeignKey(a => a.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(a => a.CustomerId).HasDatabaseName("ix_addresses_customerid");
                entity.HasIndex(a => new { a.CustomerId, a.IsDefaultBilling }).HasDatabaseName("ix_addresses_default_billing");
                entity.HasIndex(a => new { a.CustomerId, a.IsDefaultShipping }).HasDatabaseName("ix_addresses_default_shipping");
            });

            // ORDER ITEM
            model.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");
                entity.HasKey(oi => oi.Id);

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.Items)
                      .HasForeignKey(oi => oi.OrderId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.ProductId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(oi => oi.CodeSnapshot)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(oi => oi.ProductNameSnapshot)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(oi => oi.UnitPrice)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.Property(oi => oi.Quantity)
                      .IsRequired();

                entity.Property(oi => oi.TaxRate)
                      .IsRequired()
                      .HasColumnType("decimal(5,4)");

                entity.Property(oi => oi.LineTotal)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");
            });

            model.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.HasKey(o => o.Id);

                entity.Property(o => o.OrderNumber).IsRequired();

                entity.Property(o => o.Status)
                      .HasConversion<string>()
                      .HasMaxLength(32)
                      .IsUnicode(false);

                entity.Property(o => o.SubTotal).HasColumnType("numeric(18,2)").IsRequired();
                entity.Property(o => o.TaxTotal).HasColumnType("numeric(18,2)").IsRequired();
                entity.Property(o => o.ShippingTotal).HasColumnType("numeric(18,2)").IsRequired();
                entity.Property(o => o.DiscountTotal).HasColumnType("numeric(18,2)").IsRequired();
                entity.Property(o => o.GrandTotal).HasColumnType("numeric(18,2)").IsRequired();

                entity.Property(o => o.Currency).IsRequired().HasMaxLength(3).IsUnicode(false);

                entity.Property(o => o.PlaceAt).IsRequired();
                entity.Property(o => o.PaidAt).IsRequired(false);
                entity.Property(o => o.CancelledAt).IsRequired(false);
                entity.Property(o => o.DeliveredAt).IsRequired(false);

                entity.HasMany(o => o.Items)
                      .WithOne(i => i.Order)
                      .HasForeignKey(i => i.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(o => o.Payments)
                      .WithOne(p => p.Order)
                      .HasForeignKey(p => p.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(o => o.Shipments)
                      .WithOne(s => s.Order)
                      .HasForeignKey(s => s.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.OwnsOne(o => o.ShippingAddressSnapshot, sa =>
                {
                    sa.WithOwner().HasForeignKey("OrderId");

                    sa.Property(x => x.Line1).HasMaxLength(200);
                    sa.Property(x => x.Line2).HasMaxLength(200);
                    sa.Property(x => x.City).HasMaxLength(120);
                    sa.Property(x => x.State).HasMaxLength(120);
                    sa.Property(x => x.PostalCode).HasMaxLength(20).IsUnicode(false);
                    sa.Property(x => x.Country).HasMaxLength(2).IsUnicode(false);
                });

                entity.OwnsOne(o => o.BillingAddressSnapshot, ba =>
                {
                    ba.WithOwner().HasForeignKey("OrderId");

                    ba.Property(x => x.Line1).HasMaxLength(200);
                    ba.Property(x => x.Line2).HasMaxLength(200);
                    ba.Property(x => x.City).HasMaxLength(120);
                    ba.Property(x => x.State).HasMaxLength(120);
                    ba.Property(x => x.PostalCode).HasMaxLength(20).IsUnicode(false);
                    ba.Property(x => x.Country).HasMaxLength(2).IsUnicode(false);
                });

                entity.Navigation(o => o.ShippingAddressSnapshot).IsRequired();
                entity.Navigation(o => o.BillingAddressSnapshot).IsRequired();

                entity.HasIndex(o => o.OrderNumber).HasDatabaseName("ix_orders_ordernumber").IsUnique(false);
                entity.HasIndex(o => new { o.PlaceAt, o.Status }).HasDatabaseName("ix_orders_placeat_status");
            });

            // PAYMENT
            model.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");
                entity.HasKey(p => p.Id);

                entity.HasOne(p => p.Order)
                      .WithMany(o => o.Payments)
                      .HasForeignKey(p => p.OrderId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(p => p.Amount)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.Property(p => p.Currency)
                      .IsRequired()
                      .HasMaxLength(3);

                entity.Property(p => p.ExternalReference)
                      .HasMaxLength(255);

                entity.Property(p => p.OcurredAt)
                      .IsRequired();

                entity.Property(p => p.Provider)
                      .HasConversion<string>()
                      .IsRequired();

                entity.Property(p => p.Status)
                      .HasConversion<string>()
                      .IsRequired();
            });

            // SHIPMENT
            model.Entity<Shipment>(entity =>
            {
                entity.ToTable("shipments");
                entity.HasKey(s => s.Id);

                entity.HasOne(s => s.Order)
                      .WithMany(o => o.Shipments)
                      .HasForeignKey(s => s.OrderId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(s => s.Carrier)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(s => s.TrackingNumber)
                      .HasMaxLength(100);

                entity.Property(s => s.ShippedAt)
                      .IsRequired();

                entity.Property(s => s.DeliveredAt)
                      .IsRequired(false);

                entity.Property(s => s.Cost)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.Property(s => s.Status)
                      .HasConversion<string>()
                      .IsRequired();
            });

            // REVIEW
            model.Entity<Review>(entity =>
            {
                entity.ToTable("reviews");
                entity.HasKey(r => r.Id);

                entity.HasOne(r => r.Product)
                      .WithMany(p => p.Reviews)
                      .HasForeignKey(r => r.ProductId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Customer)
                      .WithMany(c => c.Reviews)
                      .HasForeignKey(r => r.CustomerId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(r => r.Rating).IsRequired();
                entity.Property(r => r.Comment).HasMaxLength(1000).IsRequired(false);
                entity.Property(r => r.isApproved).IsRequired();
                entity.Property(r => r.CreatedAt).IsRequired();
            });

            ApplyPrefixedSnakeCaseColumnNames(model);
        }

        private static void ApplyPrefixedSnakeCaseColumnNames(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (string.IsNullOrWhiteSpace(tableName))
                    continue;

                var prefix = SingularizeSnake(tableName);

                foreach (var property in entity.GetProperties())
                {
                    var baseName = ToSnakeCase(property.Name);
                    var finalBase = baseName.StartsWith(prefix + "_") ? baseName : $"{prefix}_{baseName}";
                    property.SetColumnName(finalBase);
                }

                foreach (var key in entity.GetKeys())
                {
                    foreach (var p in key.Properties)
                    {
                        var baseName = ToSnakeCase(p.Name);
                        var finalBase = baseName.StartsWith(prefix + "_") ? baseName : $"{prefix}_{baseName}";
                        p.SetColumnName(finalBase);
                    }
                }

                foreach (var fk in entity.GetForeignKeys())
                {
                    foreach (var p in fk.Properties)
                    {
                        var baseName = ToSnakeCase(p.Name);
                        var finalBase = baseName.StartsWith(prefix + "_") ? baseName : $"{prefix}_{baseName}";
                        p.SetColumnName(finalBase);
                    }
                }
            }
        }

        private static string SingularizeSnake(string snakeTable)
        {
            var parts = snakeTable.Split('_');
            if (parts.Length == 0) return snakeTable;

            var last = parts[^1];
            string singularLast;
            if (last.EndsWith("ies"))
                singularLast = last[..^3] + "y";
            else if (last.EndsWith("ses") || last.EndsWith("xes") || last.EndsWith("zes") || last.EndsWith("ches") || last.EndsWith("shes"))
                singularLast = last[..^2];
            else if (last.EndsWith("s"))
                singularLast = last[..^1];
            else
                singularLast = last;

            parts[^1] = singularLast;
            return string.Join('_', parts);
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var sb = new StringBuilder();
            var prevCategory = default(UnicodeCategory?);
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsUpper(c))
                {
                    if (i > 0 && (prevCategory == UnicodeCategory.LowercaseLetter || i + 1 < input.Length && char.IsLower(input[i + 1])))
                        sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
                prevCategory = char.GetUnicodeCategory(c);
            }
            return sb.ToString();
        }
    }
}
