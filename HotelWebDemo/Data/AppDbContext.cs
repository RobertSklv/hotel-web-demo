﻿using HotelWebDemo.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data;

public class AppDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AdminUser> AdminUsers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingCancellation> BookingCancellations { get; set; }
    public DbSet<BookingCustomer> BookingCustomers { get; set; }
    public DbSet<BookingItem> BookingItems { get; set; }
    public DbSet<BookingItemRoomFeature> BookingItemRoomFeatures { get; set; }
    public DbSet<BookingPayment> BookingPayments { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAccount> CustomerAccounts { get; set; }
    public DbSet<CustomerIdentity> CustomerIdentities { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<HotelReview> HotelReviews { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomCategory> RoomCategories { get; set; }
    public DbSet<RoomFeature> RoomFeatures { get; set; }
    public DbSet<RoomFeatureRoom> RoomFeatureRooms { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookingCustomer>()
            .HasOne(e => e.Customer)
            .WithMany(e => e.BookingCustomers)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<BookingCustomer>()
            .HasOne(e => e.Booking)
            .WithMany(e => e.BookingCustomers)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<RoomFeatureRoom>()
            .HasOne(e => e.Room)
            .WithMany(e => e.RoomFeatureRooms)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<RoomFeatureRoom>()
            .HasOne(e => e.RoomFeature)
            .WithMany(e => e.RoomFeatureRooms)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<BookingItem>()
            .HasOne(e => e.Booking)
            .WithMany(e => e.BookingItems)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<BookingItemRoomFeature>()
            .HasOne(e => e.BookingItem)
            .WithMany(e => e.DesiredFeatures)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<BookingItemRoomFeature>()
            .HasOne(e => e.RoomFeature)
            .WithMany(e => e.BookedFeatures)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Review>()
            .HasOne(e => e.Author)
            .WithMany(e => e.Reviews)
            .OnDelete(DeleteBehavior.NoAction);
    }

    protected void SetTimestamps()
    {
        var now = DateTime.UtcNow;

        foreach (var changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is BaseEntity baseEntity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        baseEntity.CreatedAt = now;
                        baseEntity.UpdatedAt = now;
                        break;

                    case EntityState.Modified:
                        Entry(baseEntity).Property(x => x.CreatedAt).IsModified = false;
                        baseEntity.UpdatedAt = now;
                        break;
                }
            }
        }
    }

    public override int SaveChanges()
    {
        SetTimestamps();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestamps();

        return base.SaveChangesAsync(cancellationToken);
    }
}