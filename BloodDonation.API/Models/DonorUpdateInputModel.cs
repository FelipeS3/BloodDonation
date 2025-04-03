using BloodDonation.API.Entities;
using BloodDonation.API.Eum;

namespace BloodDonation.API.Models;

public class DonorUpdateInputModel
{
    public string? FullName { get; private set; }
    public string? Email { get; private set; }
    public double Weight { get; private set; }
}