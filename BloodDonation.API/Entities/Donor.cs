using BloodDonation.API.Eum;

namespace BloodDonation.API.Entities;

public class Donor : BaseEntity
{
    public Donor(string fullName, string email, DateTime birthDate, Gender gender, double weight, string bloodType, string rhFactor)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Gender = gender;
        Weight = weight;
        BloodType = bloodType;
        RhFactor = rhFactor;
        Donations = [];
        Address = null!;

        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Full name cannot be empty.", nameof(fullName));

        if (weight < 50) throw new ArgumentException("Minimum weight must be 50 kilos.");


    }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public double Weight { get; private set; }
    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }
    public Address Address { get; private set; }
    public List<Donation> Donations { get; private set; }
    
    public int Age => DateTime.Today.Year - BirthDate.Year - (DateTime.Today.DayOfYear < BirthDate.DayOfYear ? 1 : 0);

    private bool CanDonate()
    {
        if (Weight < 50) return false;
        if (Age < 18) return false;

        var lastDonation = Donations.OrderByDescending(d => d.DonationDate).FirstOrDefault();

        if (lastDonation != null)
        {
            var daysSinceLast = (DateTime.Now - lastDonation.DonationDate).TotalDays;

            if (Gender == Gender.Female && daysSinceLast < 90) return false;
            if (Gender == Gender.Male && daysSinceLast < 60) return false;
        }

        return true;
    }
}