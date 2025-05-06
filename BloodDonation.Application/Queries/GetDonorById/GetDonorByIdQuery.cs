using BloodDonation.Application.Models;
using MediatR;

namespace BloodDonation.Application.Queries.GetDonorById;

public class GetDonorByIdQuery : IRequest<ResultViewModel<DonorDetailsViewModel>>
{
    public GetDonorByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}