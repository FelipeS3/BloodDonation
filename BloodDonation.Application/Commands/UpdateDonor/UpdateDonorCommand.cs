using BloodDonation.Application.Models;
using MediatR;

namespace BloodDonation.Application.Commands.UpdateDonor;

public class UpdateDonorCommand : IRequest<ResultViewModel>
{
    public UpdateDonorCommand(int id)
    {
        DonorId = id;
    }
    public int DonorId { get; private set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public double Weight { get; set; }

}