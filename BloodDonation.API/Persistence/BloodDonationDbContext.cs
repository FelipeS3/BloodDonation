using BloodDonation.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.API.Data;

public class BloodDonationDbContext : DbContext
{
    public BloodDonationDbContext(DbContextOptions<BloodDonationDbContext> options) : base(options)
    { }

    public DbSet<Donor> Donors { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<BloodStock> BloodStocks { get; set; }
    public DbSet<Address> Addresses { get; set; }  

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Donor>(e=>
        {
            e.HasKey(d => d.Id);

            e.HasMany(d => d.Donations)
                .WithOne(d => d.Donor)
                .HasForeignKey(d => d.DonorId);
        });

        builder.Entity<Donation>(e =>
        {
            e.HasKey(d => d.Id);

            e.HasOne(d => d.Donor)
                .WithMany(d => d.Donations)
                .HasForeignKey(d => d.DonorId);
        });

        builder.Entity<BloodStock>(e =>
        {
            e.HasKey(b => b.Id);
        });

        builder.Entity<Address>(e =>
        {
            e.HasKey(a => a.Id);

            e.HasOne(b => b.Donor)
                .WithOne(b => b.Address)
                .HasForeignKey<Address>(a => a.DonorId);
        });
    }
}