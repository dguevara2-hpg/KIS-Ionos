using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class Analytics_Search_Term_By_User
{
    public required string Term { get; set; }
    public string HealthSystem { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public DateTime RecentVisit { get; set; }
    public int Searches { get; set; }
}