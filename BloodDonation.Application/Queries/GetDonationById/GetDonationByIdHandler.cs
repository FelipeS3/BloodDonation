using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Queries.GetDonationById;

public class GetDonationByIdHandler : IRequestHandler<GetDonationsByIdQuery, ResultViewModel<DonationDetailsViewModel>>
{
    private readonly BloodDonationDbContext _context;
    public GetDonationByIdHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<DonationDetailsViewModel>> Handle(GetDonationsByIdQuery request, CancellationToken cancellationToken)
    {
        var donation = await _context.Donations.Include(d => d.Donor).SingleOrDefaultAsync(d => d.Id == request.Id);

        if (donation == null)
        {
            return ResultViewModel<DonationDetailsViewModel>.Error("No donations registered yet");
        }

        var model = DonationDetailsViewModel.FromEntity(donation);

        return ResultViewModel<DonationDetailsViewModel>.Success(model);
    }
}