using System;
using System.Collections.Generic;

namespace JSXDBL.Models;

public partial class Cars
{
    public int MakeFk { get; set; }

    public string Model { get; set; } = null!;

    public int FuelFk { get; set; }

    public int GearFk { get; set; }

    public int CarSk { get; set; }

    public virtual Fuel FuelFkNavigation { get; set; } = null!;

    public virtual Gear GearFkNavigation { get; set; } = null!;

    public virtual Make MakeFkNavigation { get; set; } = null!;
}
