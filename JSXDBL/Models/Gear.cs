using System;
using System.Collections.Generic;

namespace JSXDBL.Models;

public partial class Gear
{
    public int GearSk { get; set; }

    public string GearName { get; set; } = null!;

    public virtual ICollection<Cars> Cars { get; } = new List<Cars>();
}
