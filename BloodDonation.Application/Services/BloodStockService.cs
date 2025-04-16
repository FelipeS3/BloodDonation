using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.Application.Services;

public class BloodStockService : IBloodStockService
{
    private readonly BloodDonationDbContext _context;

    public BloodStockService(BloodDonationDbContext context)
    {
        _context = context;
    }

    public ResultViewModel<List<BloodStockViewModel>> GetAll()
    {
        var stocks = _context.BloodStocks.ToList();

        var model = stocks.Select(d => BloodStockViewModel.FromEntity(d)).ToList();

        if (stocks.IsNullOrEmpty())
        {
            return ResultViewModel<List<BloodStockViewModel>>.Error("Empty stock");
        }

        return ResultViewModel<List<BloodStockViewModel>>.Success(model);
    }

    public ResultViewModel<BloodStockViewModel> Get(string? blood = "", string? rhFactor = "")
    {
        var stock = _context.BloodStocks.FirstOrDefault(x => x.BloodType == blood || x.RhFactor == rhFactor);

        if (stock == null)
        {
            return ResultViewModel<BloodStockViewModel>.Error("No stock found for given blood type and Rh factor.");
        }

        var model = BloodStockViewModel.FromEntity(stock);

        return ResultViewModel<BloodStockViewModel>.Success(model);
    }
}