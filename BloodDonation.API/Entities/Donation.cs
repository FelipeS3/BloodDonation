namespace BloodDonation.API.Entities;

public class Donation : BaseEntity
{
    public Donation(int idDonor, int quantityMl)
    {
        IdDonor = idDonor;
        VolumeInML = quantityMl;
        DonationDate = DateTime.UtcNow;

        ValidateVolume();
    }
    public int VolumeInML { get; private set; }
    public DateTime DonationDate { get; private set; }
    public int IdDonor { get; private set; }
    public Donor Donor { get; private set; }

    private void ValidateVolume()
    {
        if (VolumeInML is < 420 or > 470)
        {
            throw new ArgumentException("Blood donation volume must be between 420ml and 470ml");
        }
    }
} 