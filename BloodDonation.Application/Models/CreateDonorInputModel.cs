using BloodDonation.Core.Entities;
using BloodDonation.Core.Enum;

namespace BloodDonation.Application.Models;

public record CreateDonorInputViewModel
{
    public CreateDonorInputViewModel(string fullName, string email, DateTime birthDate, Gender gender, double weight, string bloodType, string rhFactor)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        BloodType = bloodType;
        RhFactor = rhFactor;

        Address = null!;
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public double Weight { get; private set; }
    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }
    public Address Address { get; private set; }

    public Donor ToEntity() => new(FullName, Email, BirthDate, Gender, Weight, BloodType, RhFactor);
}