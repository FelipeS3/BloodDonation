using BloodDonation.Core.Entities;
using BloodDonation.Core.Enum;

namespace BloodDonation.Application.Models;

public record DonorDetailsViewModel
{
    public DonorDetailsViewModel(int id, string fullName, string email, DateTime birthDate, Gender gender, double weight, string bloodType, string rhFactor, List<DonationViewModel> donations)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        BloodType = bloodType;
        RhFactor = rhFactor;
        Donations = donations;
    }
    public int Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public double Weight { get; private set; }
    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }
    public List<DonationViewModel> Donations { get; set; }

    public static DonorDetailsViewModel FromEntity(Donor donor) => new(
        donor.Id,
        donor.FullName,
        donor.Email,
        donor.BirthDate,
        donor.Gender,
        donor.Weight,
        donor.BloodType,
        donor.RhFactor,
        donor.Donations.Select(DonationViewModel.FromEntity).ToList()
    );
}