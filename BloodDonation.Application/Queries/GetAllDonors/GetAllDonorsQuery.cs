using BloodDonation.Application.Models;
using MediatR;

namespace BloodDonation.Application.Queries.GetAllDonors;

public class GetAllDonorsQuery : IRequest<ResultViewModel<List<DonorViewModel>>>
{
    
}