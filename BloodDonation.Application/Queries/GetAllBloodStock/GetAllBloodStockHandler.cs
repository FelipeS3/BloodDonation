using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.Application.Queries.GetAllBloodStock;

public class GetAllBloodStockHandler : IRequestHandler<GetAllBloodStockQuery, ResultViewModel<List<BloodStockViewModel>>>
{
    private readonly BloodDonationDbContext _context;
    public GetAllBloodStockHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<List<BloodStockViewModel>>> Handle(GetAllBloodStockQuery request, CancellationToken cancellationToken)
    {
        var stocks = await _context.BloodStocks.ToListAsync();

        var model = stocks.Select(d => BloodStockViewModel.FromEntity(d)).ToList();

        if (stocks.IsNullOrEmpty())
        {
            return ResultViewModel<List<BloodStockViewModel>>.Error("Empty stock");
        }

        return ResultViewModel<List<BloodStockViewModel>>.Success(model);
    }
}