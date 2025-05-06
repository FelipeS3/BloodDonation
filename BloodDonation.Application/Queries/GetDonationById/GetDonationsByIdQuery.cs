using BloodDonation.Application.Models;
using MediatR;

namespace BloodDonation.Application.Queries.GetDonationById;

public class GetDonationsByIdQuery : IRequest<ResultViewModel<DonationDetailsViewModel>>
{
    public GetDonationsByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}