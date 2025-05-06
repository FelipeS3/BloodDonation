using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.Application.Queries.GetAllDonations;

public class GetAllDonationsHandler : IRequestHandler<GetAllDonationsQuery, ResultViewModel<List<DonationViewModel>>>
{
    private readonly BloodDonationDbContext _context;

    public GetAllDonationsHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<List<DonationViewModel>>> Handle(GetAllDonationsQuery request, CancellationToken cancellationToken)
    {
        var donation = await _context.Donations.Include(d => d.Donor).ToListAsync();

        if (donation.IsNullOrEmpty())
        {
            return ResultViewModel<List<DonationViewModel>>.Error("No donations registered yet");
        }

        var donationsView = donation.Select(d => DonationViewModel.FromEntity(d)).ToList();

        return ResultViewModel<List<DonationViewModel>>.Success(donationsView);
    }
}