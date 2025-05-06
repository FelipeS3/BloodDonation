using BloodDonation.Application.Models;
using BloodDonation.Application.Services;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Commands.DeleteDonor;

public class DeleteDonorHandler : IRequestHandler<DeleteDonorCommand, ResultViewModel>
{
    private readonly BloodDonationDbContext _context;
    public DeleteDonorHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel> Handle(DeleteDonorCommand request, CancellationToken cancellationToken)
    {
        var donor = await _context.Donors.SingleOrDefaultAsync(d=>d.Id == request.Id);

        if (donor == null)
        {
            return new ResultViewModel(false, "No donors registered yet");
        }

        _context.Donors.Remove(donor);
        await _context.SaveChangesAsync();

        return new ResultViewModel(true, "Deleted");
    }
}