using BloodDonation.Application.Models;
using MediatR;

namespace BloodDonation.Application.Queries.GetAllDonations;

public class GetAllDonationsQuery : IRequest<ResultViewModel<List<DonationViewModel>>>
{
    
}