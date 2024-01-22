using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class UserRole
{
    public decimal RoleId { get; set; }

    public string? Role { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<RequestActivity> RequestActivities { get; } = new List<RequestActivity>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
