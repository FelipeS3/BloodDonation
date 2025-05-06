using BloodDonation.Application.Models;
using MediatR;

namespace BloodDonation.Application.Commands.UpdateDonor;

public class UpdateDonorCommand : IRequest<ResultViewModel>
{
    public int DonorId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public double Weight { get; set; }
}