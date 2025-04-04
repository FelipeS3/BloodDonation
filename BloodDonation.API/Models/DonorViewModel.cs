using BloodDonation.API.Entities;

namespace BloodDonation.API.Models;

public class DonorViewModel
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
    public int DonationCount { get; private set; }

    public static DonorViewModel FromEntity(Donor donor) => new(donor.Id, donor.FullName, donor.Email,donor.Donations.Count);
}