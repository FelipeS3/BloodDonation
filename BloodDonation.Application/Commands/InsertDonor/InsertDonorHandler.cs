using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Commands.InsertDonor;

public class InsertDonorHandler : IRequestHandler<InsertDonorCommand, ResultViewModel<int>>
{
    private readonly BloodDonationDbContext _context;
    public InsertDonorHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<int>> Handle(InsertDonorCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var donor = request.ToEntity();

            if (await _context.Donors.AnyAsync(d => d.Email == donor.Email))
            {
                return ResultViewModel<int>.Error("Email already exist.");
            }

            if (string.IsNullOrEmpty(donor.FullName))
            {
                return ResultViewModel<int>.Error("Full name cannot be empty.");
            }

            await _context.Donors.AddAsync(donor, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ResultViewModel<int>.Success(donor.Id);
        }


        catch (Exception ex)
        {
            return ResultViewModel<int>.Error($"Erro interno: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}