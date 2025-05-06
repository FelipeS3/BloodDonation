using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.Application.Queries.GetAllDonors;

public class GetAllDonorsHandler : IRequestHandler<GetAllDonorsQuery, ResultViewModel<List<DonorViewModel>>>
{
    private readonly BloodDonationDbContext _context;
    public GetAllDonorsHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<List<DonorViewModel>>> Handle(GetAllDonorsQuery request, CancellationToken cancellationToken)
    {
        var donors = await _context.Donors.Include(d => d.Donations).ToListAsync();

        var model = donors.Select(d => DonorViewModel.FromEntity(d)).ToList();

        if (model.IsNullOrEmpty())
        {
            return ResultViewModel<List<DonorViewModel>>.Error("No donors registered yet");
        }

        return ResultViewModel<List<DonorViewModel>>.Success(model);
    }
}