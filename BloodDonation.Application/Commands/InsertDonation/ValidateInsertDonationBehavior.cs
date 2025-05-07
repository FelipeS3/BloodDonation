using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using MediatR;

namespace BloodDonation.Application.Commands.InsertDonation;

public class ValidateInsertDonationBehavior : IPipelineBehavior<InsertDonationCommand, ResultViewModel<int>>
{
    private readonly BloodDonationDbContext _context;
    public ValidateInsertDonationBehavior(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<int>> Handle(InsertDonationCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
    {
        var donorExists = _context.Donors.Any(d => d.Id == request.DonorId);

        if (!donorExists)
        {
            return ResultViewModel<int>.Error("Invalid donor data.");
        }

        return await next();
    }
}