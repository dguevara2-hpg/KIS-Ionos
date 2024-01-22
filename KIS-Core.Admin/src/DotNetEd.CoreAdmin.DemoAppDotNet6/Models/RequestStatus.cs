using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class RequestStatus
{
    public decimal RStatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<RequestActivity> RequestActivities { get; } = new List<RequestActivity>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
