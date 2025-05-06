using BloodDonation.Application.Models;
using MediatR;

namespace BloodDonation.Application.Queries.GetBloodStockById;

public class GetBloodStockByIdQuery : IRequest<ResultViewModel<BloodStockViewModel>>
{
    public int Id { get; set; }
}