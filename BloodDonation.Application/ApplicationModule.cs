using BloodDonation.Application.Commands.InsertDonation;
using BloodDonation.Application.Commands.InsertDonor;
using BloodDonation.Application.Models;
using BloodDonation.Application.Services;
using BloodDonation.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonation.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddHandlers()
            .AddServices()
            .AddValidations();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDonorService, DonorService>();
        services.AddScoped<IDonationService, DonationService>();
        services.AddScoped<IBloodStockService, BloodStockService>();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertDonorCommand>());

        services.AddTransient<IPipelineBehavior<InsertDonationCommand, ResultViewModel<int>>,
            ValidateInsertDonationBehavior>();

        return services;
    }

    private static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssemblyContaining<InsertDonorCommandValidator>();

        return services;
    }
}