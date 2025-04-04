namespace BloodDonation.API.Entities;

public class BloodStock : BaseEntity
{
    public BloodStock(string typeBlood, string factorRh, int volumeMl)
    {
        TypeBlood = typeBlood;
        FactorRH = factorRh;
        VolumeML = volumeMl;
    }

    public string TypeBlood { get; private set; }
    public string FactorRH { get; private set; }
    public int VolumeML { get; private set; }
}

