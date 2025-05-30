using BloodDonation.API.ExceptionHandler;
using BloodDonation.Application;
using BloodDonation.Application.Commands.InsertDonor;
using BloodDonation.Application.Filters;
using BloodDonation.Application.Validators;
using BloodDonation.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("BloodDonationCs");
            builder.Services.AddDbContext<BloodDonationDbContext>(x=>x.UseSqlServer(connectionString));

            builder.Services.AddApplication();

            builder.Services.AddExceptionHandler<ApiExceptionHandler>();

            builder.Services.AddProblemDetails();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
