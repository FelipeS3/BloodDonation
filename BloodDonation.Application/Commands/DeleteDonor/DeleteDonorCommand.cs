using BloodDonation.Application.Models;
using MediatR;

namespace BloodDonation.Application.Commands.DeleteDonor;

public class DeleteDonorCommand : IRequest<ResultViewModel>
{
    public DeleteDonorCommand(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}