using BloodDonation.Application.Models;
using BloodDonation.Core.Entities;
using BloodDonation.Core.Enum;
using MediatR;

namespace BloodDonation.Application.Commands.InsertDonor;

public class InsertDonorCommand : IRequest<ResultViewModel<int>>
{
    public string FullName { get;  set; }
    public string Email { get;  set; }
    public DateTime BirthDate { get;  set; }
    public Gender Gender { get;  set; }
    public double Weight { get;  set; }
    public string BloodType { get;  set; }
    public string RhFactor { get;  set; }

    public Donor ToEntity() => new(FullName, Email, BirthDate, Gender, Weight, BloodType, RhFactor);
}