using BloodDonation.Application.Models;

namespace BloodDonation.Application.Services;

public interface IDonorService
{
    ResultViewModel<List<DonorViewModel>> GetAll();
    ResultViewModel<DonorDetailsViewModel> GetById(int id);
    ResultViewModel<int> Insert(CreateDonorInputViewModel input);
    ResultViewModel Update(int id, DonorUpdateInputModel update);
    ResultViewModel Delete(int id);
}