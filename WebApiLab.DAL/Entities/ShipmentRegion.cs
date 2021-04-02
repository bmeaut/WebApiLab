using System;

namespace WebApiLab.DAL.Entities
{
    [Flags]
    public enum ShipmentRegion
    {
        EU = 1,
        NorthAmerica = 2,
        Asia = 4,
        Australia = 8
    }
}
