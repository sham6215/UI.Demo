using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGrid.Models
{
    public enum CollectionShowStatus : int
    {
        All = 0,
        InCollection,
        NotInCollection
    }

    public enum TpShowStatus : int
    {
        All = 0,
        Cancelled,
        InForce
    }
}
