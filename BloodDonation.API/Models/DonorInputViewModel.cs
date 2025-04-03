using BloodDonation.API.Entities;
using BloodDonation.API.Eum;

namespace BloodDonation.API.Models;

public record DonorInputViewModel
{
    public DonorInputViewModel(string fullName, string email, DateTime birthDate, Gender gender, double weight, string bloodType, string rhFactor)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        BloodType = bloodType;
        RhFactor = rhFactor;
    }
    public string FullName { get;  set; }
    public string Email { get;  set; }
    public DateTime BirthDate { get;  set; }
    public Gender Gender { get; set; }
    public double Weight { get; set; }
    public string BloodType { get; set; }
    public string RhFactor { get; set; }

    public static DonorInputViewModel ToEntity(Donor donor) => new(donor.FullName, donor.Email, donor.BirthDate,
        donor.Gender, donor.Weight, donor.BloodType, donor.RhFactor);
}