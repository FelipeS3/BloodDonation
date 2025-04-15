using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.Application.Services;

public class DonorService : IDonorService
{
    private readonly BloodDonationDbContext _context;
    public DonorService(BloodDonationDbContext context)
    {
        _context = context;
    }

    public ResultViewModel<List<DonorViewModel>> GetAll()
    {
        var donors = _context.Donors.Include(d => d.Donations).ToList();

        var model = donors.Select(d => DonorViewModel.FromEntity(d)).ToList();

        if (model.IsNullOrEmpty())
        {
            return ResultViewModel<List<DonorViewModel>>.Error("No donors registered yet");
        }

        return ResultViewModel<List<DonorViewModel>>.Success(model);
    }

    public ResultViewModel<DonorDetailsViewModel> GetById(int id)
    {
        var donor = _context.Donors.Include(d => d.Donations).FirstOrDefault(d => d.Id == id);

        if (donor == null)
        {
            return ResultViewModel<DonorDetailsViewModel>.Error("Donor Not Found");
        }

        var model = DonorDetailsViewModel.FromEntity(donor);

        return ResultViewModel<DonorDetailsViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateDonorInputViewModel input)
    {
            var donor = input.ToEntity();

        if (_context.Donors.Any(d => d.Email == donor.Email)) 
        { 
            return ResultViewModel<int>.Error("Email already exist.");
        }

            if (string.IsNullOrEmpty(donor.FullName))
            {
                return ResultViewModel<int>.Error("Name cannot be empty.");
            }

            _context.Donors.Add(donor);
            _context.SaveChanges();


            return ResultViewModel<int>.Success(donor.Id);
    }

    public ResultViewModel Update(int id, DonorUpdateInputModel update)
    {
        var donor = _context.Donors.Find(id);
        if (donor == null)
        {
            return new ResultViewModel(false, "No donors registered yet");
        }

        if (update.Weight < 50)
        {
            return new ResultViewModel(false,"Minimum weight of 50 kg");
        }

        donor.Update(update.FullName, update.Email, update.Weight);
        _context.SaveChanges();

        return new ResultViewModel();
    }

    public ResultViewModel Delete(int id)
    {
        var donor = _context.Donors.Find(id);

        if (donor == null)
        {
            return new ResultViewModel(false,"No donors registered yet");
        }

        _context.Donors.Remove(donor);
        _context.SaveChanges();

        return new ResultViewModel();
    }
}