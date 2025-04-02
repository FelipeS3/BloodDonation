namespace BloodDonation.API.Models;

public record DonationInputViewModel
{
    public DonationInputViewModel(int donorId, int volumeInMl)
    {
        DonorId = donorId;
        VolumeInML = volumeInMl;

        DonationTime = DateTime.UtcNow;
    }
    public int DonorId { get; private set; }
    public int VolumeInML { get; private set; }
    public DateTime DonationTime { get; private set; }
}