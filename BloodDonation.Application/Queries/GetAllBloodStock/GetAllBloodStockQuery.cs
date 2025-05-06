using BloodDonation.Application.Models;
using MediatR;

namespace BloodDonation.Application.Queries.GetAllBloodStock;

public class GetAllBloodStockQuery : IRequest<ResultViewModel<List<BloodStockViewModel>>>
{
    
}