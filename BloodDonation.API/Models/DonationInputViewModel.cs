using BloodDonation.API.Entities;

namespace BloodDonation.API.Models;

public record DonationInputViewModel
{
    public DonationInputViewModel(int id, int donorId, string donorName, int volumeInMl)
    {
        Id = id;
        DonorId = donorId;
        DonorName = donorName;
        VolumeInML = volumeInMl;

        DonationTime = DateTime.UtcNow;
    }
    public int Id { get; private set; }
    public int DonorId { get; private set; }
    public string DonorName { get; private set; }
    public int VolumeInML { get; private set; }
    public DateTime DonationTime { get; private set; }

    public static DonationInputViewModel FromEntity(Donation donation) => new(donation.Id, donation.DonorId, donation.Donor.FullName,donation.VolumeInML);
}