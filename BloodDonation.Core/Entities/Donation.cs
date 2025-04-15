namespace BloodDonation.Core.Entities;

public class Donation : BaseEntity
{
    public Donation(int donorId, int volumeInMl)
    {
        DonorId = donorId;
        VolumeInMl = volumeInMl;
        DonationDate = DateTime.UtcNow;
    }
    public int VolumeInMl { get; private set; }
    public DateTime DonationDate { get; private set; }
    public int DonorId { get; private set; }
    public Donor Donor { get; private set; }
} 