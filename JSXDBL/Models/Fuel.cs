using System;
using System.Collections.Generic;

namespace JSXDBL.Models;

public partial class Fuel
{
    public int FuelSk { get; set; }

    public string FuelName { get; set; } = null!;

    public virtual ICollection<Cars> Cars { get; } = new List<Cars>();
}
