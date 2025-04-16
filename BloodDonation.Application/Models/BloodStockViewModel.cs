using BloodDonation.Core.Entities;

namespace BloodDonation.Application.Models;

public class BloodStockViewModel
{
    public BloodStockViewModel(string bloodType, string rhFactor, int volumeInMl)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        VolumeInMl = volumeInMl;
    }

    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }
    public int VolumeInMl { get; private set; }

    public static BloodStockViewModel FromEntity(BloodStock stock) =>
        new(stock.BloodType, stock.RhFactor, stock.VolumeInMl);
}