using BloodDonation.API.Entities;

namespace BloodDonation.API.Models;

public class CreateDonationInputModel
{
    public CreateDonationInputModel(int donorId, int volumeInMl)
    {
        DonorId = donorId;
        VolumeInML = volumeInMl;
    }

    public int DonorId { get; private set; }
    public int VolumeInML { get; private set; }

    public Donation ToEntity() => new(DonorId, VolumeInML);
}