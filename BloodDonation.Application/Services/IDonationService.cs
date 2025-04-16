using BloodDonation.Application.Models;

namespace BloodDonation.Application.Services;

public interface IDonationService
{
    ResultViewModel<List<DonationViewModel>> GetAll();
    ResultViewModel<DonationDetailsViewModel> Get(int id);
    ResultViewModel<int> Insert(CreateDonationInputModel input);
}