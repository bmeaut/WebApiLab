using System;

namespace WebApiLab.Entities
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
