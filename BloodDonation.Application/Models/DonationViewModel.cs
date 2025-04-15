using BloodDonation.Core.Entities;

namespace BloodDonation.Application.Models;

public record DonationViewModel
{
    public DonationViewModel(int id,int volumeInMl)
    {
        Id = id;
        VolumeInMl = volumeInMl;
        DonationTime = DateTime.UtcNow;
    }
    public int Id { get; private set; }
    public int VolumeInMl { get; private set; }
    public DateTime DonationTime { get; private set; }

    public static DonationViewModel FromEntity(Donation donation) => new(donation.Id, donation.VolumeInMl);
}