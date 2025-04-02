using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;

namespace BloodDonation.API.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        
    }
    public int Id { get; private set; }
}