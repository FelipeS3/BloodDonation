using BloodDonation.Application.Models;
using BloodDonation.Core.Entities;
using BloodDonation.Core.Enum;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Commands.InsertDonation;

public class InsertDonationHandler : IRequestHandler<InsertDonationCommand, ResultViewModel<int>>
{
    private readonly BloodDonationDbContext _context;
    public InsertDonationHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<int>> Handle(InsertDonationCommand request, CancellationToken cancellationToken)
    {
        var donor = await _context.Donors.Include(d => d.Donations).FirstOrDefaultAsync(d => d.Id == request.DonorId);

        if (donor == null)
        {
            return ResultViewModel<int>.Error("Donor not found.");
        }

        if (donor.Age < 18)
        {
            return ResultViewModel<int>.Error("Donor must be at least 18 years old to donate.");
        }

        if (donor.Weight < 50)
        {
            return ResultViewModel<int>.Error("Minimum weight must be 50 kilos to donate.");
        }

        if (request.VolumeInML is < 420 or > 470)
        {
            return ResultViewModel<int>.Error("Blood donation volume must be between 420ml and 470ml");
        }

        //var lastDonation = donor.Donations.OrderByDescending(d => d.DonationDate).FirstOrDefault();

        //if (lastDonation != null)
        //{
        //    var daysSinceLast = (DateTime.Now - lastDonation.DonationDate).TotalDays;

        //    if (donor.Gender == Gender.Female && daysSinceLast < 90)
        //    {
        //        return ResultViewModel<int>.Error("Female can only donate after 90 days");
        //    }

        //    if (donor.Gender == Gender.Male && daysSinceLast < 60)
        //    {
        //        return ResultViewModel<int>.Error("Males can only donate after 60 days");
        //    }
        //}

        var donation = request.ToEntity();
        await _context.Donations.AddAsync(donation);

        var bloodStock =
            await _context.BloodStocks.FirstOrDefaultAsync(x =>
                x.BloodType == donor.BloodType && x.RhFactor == donor.RhFactor);

        if (bloodStock == null)
        {
            var stock = new BloodStock(donor.BloodType, donor.RhFactor, request.VolumeInML);
            await _context.BloodStocks.AddAsync(stock);
        }

        if (bloodStock != null)
        {
            bloodStock.AddVolume(request.VolumeInML);
        }

        await _context.SaveChangesAsync();

        return ResultViewModel<int>.Success(donation.DonorId);

    }
}