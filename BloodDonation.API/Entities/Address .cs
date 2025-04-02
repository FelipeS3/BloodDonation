namespace BloodDonation.API.Entities;

public class Address : BaseEntity
{
    public Address(string street, string city, string state, string zipCode, Donor donor)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Donor = donor;
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }
    public Donor Donor { get; private set; }
}