using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class AdminAcount
{
    public int AdminId { get; set; }

    public string? EmailAddress { get; set; }

    public string? Password { get; set; }
}
