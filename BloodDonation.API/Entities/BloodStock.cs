﻿namespace BloodDonation.API.Entities;

public class BloodStock : BaseEntity
{
    public BloodStock(string bloodType, string rhFactor, int volumeInMl)
    {
        BloodType = bloodType;
        RhFactor = rhFactor;
        VolumeInML = volumeInMl;
    }

    public string BloodType { get; private set; }
    public string RhFactor { get; private set; }
    public int VolumeInML { get; private set; }
}

