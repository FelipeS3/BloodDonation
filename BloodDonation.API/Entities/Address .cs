namespace BloodDonation.API.Entities;

public class Address : BaseEntity
{
    public Address(string street, string city, string state, string zipCode, int donorId)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        DonorId = donorId;
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }
    public int DonorId { get; private set; }
    public Donor Donor { get; private set; }
}