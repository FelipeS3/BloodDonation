using BloodDonation.Core.Entities;

namespace BloodDonation.Application.Models;

public record DonorViewModel
{
    public DonorViewModel(int id, string fullName, string email, int donationsCount)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        DonationCount = donationsCount;
    }
    public int Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public int DonationCount { get; set; }

    public static DonorViewModel FromEntity(Donor donor) => new(donor.Id, donor.FullName, donor.Email,donor.Donations.Count);
}