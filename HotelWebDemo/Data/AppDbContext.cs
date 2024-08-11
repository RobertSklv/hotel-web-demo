using HotelWebDemo.Data.Seeding;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data;

public class AppDbContext : DbContext
{
    private readonly IServiceProvider serviceProvider;

    public DbSet<Address> Addresses { get; set; }
    public DbSet<AdminRole> AdminRoles { get; set; }
    public DbSet<AdminUser> AdminUsers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingContact> BookingContacts { get; set; }
    public DbSet<BookingCancellation> BookingCancellations { get; set; }
    public DbSet<BookingCustomer> BookingCustomers { get; set; }
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
    public DbSet<RoomReservation> RoomReservations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options, IServiceProvider serviceProvider)
        : base(options)
    {
        this.serviceProvider = serviceProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Room>()
            .HasMany(e => e.Features)
            .WithMany(e => e.Rooms)
            .UsingEntity<RoomFeatureRoom>(
                l => l.HasOne(e => e.RoomFeature).WithMany(e => e.RoomFeatureRooms),
                r => r.HasOne(e => e.Room).WithMany(e => e.RoomFeatureRooms));

        SeedDefaultData(modelBuilder);
        SeedSampleData(modelBuilder);

        serviceProvider.GetRequiredService<IAdminAuthService>().CreateDefaultAdminUser();
    }

    protected void SeedDefaultData(ModelBuilder modelBuilder)
    {
        JsonSeeder<AdminRole>.SeedDefaultData(modelBuilder, "AdminRoles");
        JsonSeeder<Country>.SeedDefaultData(modelBuilder, "Countries");
    }

    protected void SeedSampleData(ModelBuilder modelBuilder)
    {
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
