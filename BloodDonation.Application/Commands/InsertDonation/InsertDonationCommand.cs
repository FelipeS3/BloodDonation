using BloodDonation.Application.Models;
using BloodDonation.Core.Entities;
using MediatR;

namespace BloodDonation.Application.Commands.InsertDonation;

public class InsertDonationCommand : IRequest<ResultViewModel<int>>
{
    public InsertDonationCommand() { }

    public int DonorId { get; set; }
    public int VolumeInML { get; set; }

    public Donation ToEntity() => new(DonorId, VolumeInML);
}