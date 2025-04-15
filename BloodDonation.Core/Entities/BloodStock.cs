namespace BloodDonation.Core.Entities;

public class BloodStock : BaseEntity
{
    public BloodStock(string bloodType, string rhFactor, int volumeInMl)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        VolumeInMl = volumeInMl;
    }
    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }
    public int VolumeInMl { get; private set; }

    public void AddVolume(int volumeInMl)
    {
        VolumeInMl += volumeInMl;
    }
}   

