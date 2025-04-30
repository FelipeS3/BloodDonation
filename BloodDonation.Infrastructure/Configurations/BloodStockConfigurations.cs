using BloodDonation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonation.Infrastructure.Configurations;

public class BloodStockConfigurations : IEntityTypeConfiguration<BloodStock>
{
    public void Configure(EntityTypeBuilder<BloodStock> builder)
    {
        builder.HasKey(b => b.Id);
    }
}