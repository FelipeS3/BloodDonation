using BloodDonation.API.Models;

namespace BloodDonation.API.Entities;

public class Donation : BaseEntity
{
    public Donation(int donorId, int volumeInML)
    {
        DonorId = donorId;
        VolumeInML = volumeInML;
        DonationDate = DateTime.UtcNow;

        if (volumeInML is < 420 or > 470)
            throw new ArgumentException("Blood donation volume must be between 420ml and 470ml");
    }
    public int VolumeInML { get; private set; }
    public DateTime DonationDate { get; private set; }
    public int DonorId { get; private set; }
    public Donor Donor { get; private set; }

} 