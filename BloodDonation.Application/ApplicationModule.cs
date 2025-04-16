using BloodDonation.Application.Services;
using BloodDonation.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service
            .AddServices();

        return service;
    }

    private static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddScoped<IDonorService, DonorService>();
        service.AddScoped<IDonationService, DonationService>();
        service.AddScoped<IBloodStockService, BloodStockService>();

        return service;
    }
}