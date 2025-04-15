using BloodDonation.Core.Entities;

namespace BloodDonation.Application.Models;

public record DonationDetailsViewModel 
{
    public DonationDetailsViewModel(int id, int volumeInMl, int donorId, string donorName, string email, DateTime donationTime)
    {
        Id = id;
        VolumeInMl = volumeInMl;
        DonationTime = donationTime;
        DonorId = donorId;
        DonorName = donorName;
        Email = email;
    }

    public int Id { get; private set; }
    public int VolumeInMl { get; private set; }
    public int DonorId { get; private set; }
    public string DonorName { get; private set; }
    public string Email { get; private set; }
    public DateTime DonationTime { get; private set; }

    public static DonationDetailsViewModel FromEntity(Donation donation) 
        => new DonationDetailsViewModel(
            donation.Id, donation.VolumeInMl, donation.DonorId, donation.Donor.FullName, donation.Donor.Email,donation.DonationDate);
}