using BloodDonation.Core.Enum;

namespace BloodDonation.Core.Entities;

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
    public List<Donation> Donations { get; private set; } = [];
    
    public int Age => DateTime.Today.Year - BirthDate.Year - (DateTime.Today.DayOfYear < BirthDate.DayOfYear ? 1 : 0);

    public void CanDonate()
    {
        if (Weight < 50) throw new ArgumentException("Minimum weight of 50kg to donate");
        if (Age < 18) throw new ArgumentException("Minimum age of 18 years old to donate");

        var lastDonation = Donations.OrderByDescending(d => d.DonationDate).FirstOrDefault();

        if (lastDonation != null)
        {
            var daysSinceLast = (DateTime.Now - lastDonation.DonationDate).TotalDays;

            if (Gender == Gender.Female && daysSinceLast < 90) throw new ArgumentException("Female can only donate after 90 days");
            if (Gender == Gender.Male && daysSinceLast < 60) throw new ArgumentException("Males can only donate after 60 days");
        }
    }

    public void Update(string fullName, string email, double weight)
    {
        FullName = fullName;
        Email = email;
        Weight = weight;
    }
}