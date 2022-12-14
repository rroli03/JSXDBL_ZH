using System;
using System.Collections.Generic;

namespace JSXDBL.Models;

public partial class Make
{
    public int MakeSk { get; set; }

    public string MakeName { get; set; } = null!;

    public virtual ICollection<Cars> Cars { get; } = new List<Cars>();
}
