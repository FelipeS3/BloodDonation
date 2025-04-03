using BloodDonation.API.Entities;
using BloodDonation.API.Eum;

namespace BloodDonation.API.Models;

public record DonorInputViewModel
{
    public string FullName { get;  set; }
    public string Email { get;  set; }
    public DateTime BirthDate { get;  set; }
    public Gender Gender { get; set; }
    public double Weight { get; set; }
    public string BloodType { get; set; }
    public string RhFactor { get; set; }
}