using BloodDonation.Core.Entities;

namespace BloodDonation.Application.Models;

public record CreateDonationInputModel
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