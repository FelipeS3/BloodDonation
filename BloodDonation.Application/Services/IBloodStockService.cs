using BloodDonation.Application.Models;

namespace BloodDonation.Application.Services;

public interface IBloodStockService
{
    ResultViewModel<List<BloodStockViewModel>> GetAll();
    ResultViewModel<BloodStockViewModel> Get(string? blood = "", string? rhFactor = "");
}