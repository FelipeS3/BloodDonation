using BloodDonation.Application.Commands.InsertDonor;
using BloodDonation.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service
            .AddHandlers()
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

    private static IServiceCollection AddHandlers(this IServiceCollection service)
    {
        service.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertDonorCommand>());

        return service;
    }
}