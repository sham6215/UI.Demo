using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGrid.Models
{
    public enum HoldingsStatus : int
    {
        All = 0,
        InCollection,
        NotInCollection
    }

    public enum TpStatus : int
    {
        All = 0,
        Cancelled,
        InForce
    }
}
