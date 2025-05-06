using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Queries.GetDonorById;

public class GetDonorByIdHandler : IRequestHandler<GetDonorByIdQuery, ResultViewModel<DonorDetailsViewModel>>
{
    private readonly BloodDonationDbContext _context;
    public GetDonorByIdHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<DonorDetailsViewModel>> Handle(GetDonorByIdQuery request, CancellationToken cancellationToken)
    {
        var donor = await _context.Donors.Include(d => d.Donations).FirstOrDefaultAsync(d => d.Id == request.Id);

        if (donor == null)
        {
            return ResultViewModel<DonorDetailsViewModel>.Error("Donor Not Found");
        }

        var model = DonorDetailsViewModel.FromEntity(donor);

        return ResultViewModel<DonorDetailsViewModel>.Success(model);
    }
}