using System.Text.RegularExpressions;
using BloodDonation.Application.Commands.InsertDonor;
using FluentValidation;

namespace BloodDonation.Application.Validators;

public class InsertDonorCommandValidator : AbstractValidator<InsertDonorCommand>
{
    public InsertDonorCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("the full name field cannot be empty or longer than 100 characters");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Invalid email format")
            .When(x=> !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage("The birthdate field cannot be empty");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .WithMessage("The gender field cannot be empty");

        RuleFor(x => x.Weight)
            .NotEmpty()
            .WithMessage("The weigh field cannot be empty");

        RuleFor(x => x.BloodType)
            .Must(ValidateBloodType)
            .WithMessage("Invalid blood type")
            .When(x=> 
                !string.IsNullOrWhiteSpace(x.BloodType) && x.BloodType.Length >= 1);

        RuleFor(x => x.RhFactor)
            .Must(ValidateRhFactor)
            .WithMessage("Invalid Rh factor. Must be '+' or '-'");
    }

    private bool ValidateBloodType(string bloodType)
    {
        var blood = new Regex("^(A|B|AB|O)$", RegexOptions.IgnoreCase);

        return blood.IsMatch(bloodType);
    }

    private bool ValidateRhFactor(string rgFactor)
    {
        var rh = new Regex("^\\+$|^\\-$");

        return rh.IsMatch(rgFactor);
    }
}