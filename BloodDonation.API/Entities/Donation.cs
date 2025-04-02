namespace BloodDonation.API.Entities;

public class Donation : BaseEntity
{
    public Donation(int idDonor, int quantityMl)
    {
        IdDonor = idDonor;
        QuantityML = quantityMl;
    }
    public int IdDonor { get; private set; }
    public Donor Donor { get; private set; }
    public int QuantityML { get; private set; }
}