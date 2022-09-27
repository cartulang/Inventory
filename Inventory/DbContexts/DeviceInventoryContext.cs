using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Inventory.Models;
using Inventory.Dtos;

namespace Inventory.DbContexts
{
    public partial class DeviceInventoryContext : DbContext
    {
        public DeviceInventoryContext()
        {
        }

        public DeviceInventoryContext(DbContextOptions<DeviceInventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<DeviceTransaction> DeviceTransactions { get; set; } = null!;
        public virtual DbSet<DevicesStatus> DevicesStatuses { get; set; } = null!;
        public virtual DbSet<Operation> Operations { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<DeviceDto> DevicesDto { get; set; } = null!;
        public virtual DbSet<DeviceTransactionDto> DeviceTransactionsDto { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=DeviceInventory;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("devices");

                entity.Property(e => e.DeviceName)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeviceTransaction>(entity =>
            {
                entity.ToTable("device_transactions");

                entity.Property(e => e.CurrentUser)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DevicesStatus>(entity =>
            {
                entity.ToTable("devices_status");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.ToTable("operation");

                entity.Property(e => e.OperationName)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
