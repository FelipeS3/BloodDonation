using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BloodDonation.Application.Commands.UpdateDonor;

public class UpdateDonorHandler : IRequestHandler<UpdateDonorCommand, ResultViewModel>
{
    private readonly BloodDonationDbContext _context;
    public UpdateDonorHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(UpdateDonorCommand request, CancellationToken cancellationToken)
    {
        var donor = await _context.Donors.FindAsync(request.DonorId);

        if (donor == null)
        {
            return new ResultViewModel(false, "No donors registered yet");
        }

        if (await _context.Donors.AnyAsync(d => d.Email == request.Email && d.Id != donor.Id))
        {
            return new ResultViewModel(false, "Email already exist.");
        }

        donor.Update(request.FullName, request.Email, request.Weight);
        await _context.SaveChangesAsync();

        return new ResultViewModel(true, "Updated!");
    }
}