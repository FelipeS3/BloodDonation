using BloodDonation.Application.Models;
using BloodDonation.Core.Entities;
using BloodDonation.Core.Enum;
using BloodDonation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.Application.Services;

public class DonationService : IDonationService
{
    private readonly BloodDonationDbContext _context;
    public DonationService(BloodDonationDbContext context)
    {
        _context = context;
    }

    public ResultViewModel<List<DonationViewModel>> GetAll()
    {
        var donation = _context.Donations.Include(d => d.Donor).ToList();

        if (donation.IsNullOrEmpty())
        {
            return ResultViewModel<List<DonationViewModel>>.Error("No donations registered yet");
        }

        var donationsView = donation.Select(d => DonationViewModel.FromEntity(d)).ToList();

        return ResultViewModel<List<DonationViewModel>>.Success(donationsView);
    }

    public ResultViewModel<DonationDetailsViewModel> Get(int id)
    {
        var donation = _context.Donations.Include(d => d.Donor).SingleOrDefault(d => d.Id == id);

        if (donation == null)
        {
            return ResultViewModel<DonationDetailsViewModel>.Error("No donations registered yet");
        }

        var model = DonationDetailsViewModel.FromEntity(donation);

        return ResultViewModel<DonationDetailsViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateDonationInputModel input)
    {
        var donor = _context.Donors.Include(d=>d.Donations).FirstOrDefault(d=>d.Id == input.DonorId);

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

        if (input.VolumeInML is < 420 or > 470)
        {
            return ResultViewModel<int>.Error("Blood donation volume must be between 420ml and 470ml");
        }

        var lastDonation = donor.Donations.OrderByDescending(d => d.DonationDate).FirstOrDefault();

        if (lastDonation != null)
        {
            var daysSinceLast = (DateTime.Now - lastDonation.DonationDate).TotalDays;
            if (donor.Gender == Gender.Female && daysSinceLast < 90)
            {
                return ResultViewModel<int>.Error("Female can only donate after 90 days");
            }

            if (donor.Gender == Gender.Male && daysSinceLast < 60)
            {
                return ResultViewModel<int>.Error("Males can only donate after 60 days");
            }
        }

        //donor.CanDonate();

        var donation = input.ToEntity();
        _context.Donations.Add(donation);

        var bloodStock =
            _context.BloodStocks.FirstOrDefault(x =>
                x.BloodType == donor.BloodType && x.RhFactor == donor.RhFactor);

        if (bloodStock == null)
        {
            var stock = new BloodStock(donor.BloodType, donor.RhFactor, input.VolumeInML);
            _context.BloodStocks.Add(stock);
        }

        if (bloodStock != null)
        {
            bloodStock.AddVolume(input.VolumeInML);
        }

        _context.SaveChanges();

        return ResultViewModel<int>.Success(donation.DonorId);
    }
}