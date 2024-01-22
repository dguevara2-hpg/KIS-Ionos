using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class Analytics_Filter_Click_By_User
{
    public required string Filter { get; set; }
    public string HealthSystem { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public DateTime RecentVisit { get; set; }
    public int Clicks { get; set; }
}