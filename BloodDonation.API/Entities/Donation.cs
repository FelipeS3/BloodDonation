using BloodDonation.API.Models;

namespace BloodDonation.API.Entities;

public class Donation : BaseEntity
{
    public Donation(int donorId, int volumeInML)
    {
        DonorId = donorId;
        VolumeInML = volumeInML;
        DonationDate = DateTime.UtcNow;
    }
    public int VolumeInML { get; private set; }
    public DateTime DonationDate { get; private set; }
    public int DonorId { get; private set; }
    public Donor Donor { get; private set; }

} 