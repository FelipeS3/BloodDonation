using System.Reflection;
using BloodDonation.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Infrastructure.Persistence;

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
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}