using System.ComponentModel.DataAnnotations;
using BloodDonation.API.Entities;
using BloodDonation.API.Eum;

namespace BloodDonation.API.Models;

public record DonorUpdateInputModel
{
    public DonorUpdateInputModel(string fullName, string email, double weight)
    {
        FullName = fullName;
        Email = email;
        Weight = weight;
    }
    public string FullName { get; private set; }
    [EmailAddress]
    public string Email { get; private set; }
    public double Weight { get; private set; }

    
}