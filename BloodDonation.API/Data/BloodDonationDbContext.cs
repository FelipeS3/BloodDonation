using BloodDonation.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.API.Data;

public class BloodDonationDbContext : DbContext
{
    public BloodDonationDbContext(DbContextOptions<BloodDonationDbContext> options) : base(options)
    { }

    public DbSet<Donor> Donors { get; private set; }
    public DbSet<Donation> Donations { get; private set; }
    public DbSet<BloodStock> BloodStocks { get; private set; }
    public DbSet<Address> Addresses { get; private set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}