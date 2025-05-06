using BloodDonation.Application.Models;
using BloodDonation.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Queries.GetBloodStockById;

public class GetBloodStockByIdHandler : IRequestHandler<GetBloodStockByIdQuery, ResultViewModel<BloodStockViewModel>>
{
    private readonly BloodDonationDbContext _context;
    public GetBloodStockByIdHandler(BloodDonationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<BloodStockViewModel>> Handle(GetBloodStockByIdQuery request, CancellationToken cancellationToken)
    {
        var stock = await _context.BloodStocks.FirstOrDefaultAsync(x=>x.Id == request.Id);

        if (stock == null)
        {
            return ResultViewModel<BloodStockViewModel>.Error("No stock found for given blood type and Rh factor.");
        }

        var model = BloodStockViewModel.FromEntity(stock);

        return ResultViewModel<BloodStockViewModel>.Success(model);
    }
}