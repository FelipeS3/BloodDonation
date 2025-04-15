namespace BloodDonation.Application.Models;

public record DonorUpdateInputModel
{
    public DonorUpdateInputModel(string fullName, string email, double weight)
    {
        FullName = fullName;
        Email = email;
        Weight = weight;
    }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public double Weight { get; private set; }

    
}