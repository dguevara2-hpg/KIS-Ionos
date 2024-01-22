using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class ProviderIdn
{
    public int Id { get; set; }

    public string? IdnName { get; set; }

    public virtual ICollection<RequestActivity> RequestActivities { get; } = new List<RequestActivity>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
