using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiLabor.Entities
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
