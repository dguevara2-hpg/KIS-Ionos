using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class Analytics_Filter_Click_By_IDN
{
    public required string Filter { get; set; }
    public required string HealthSystem { get; set; }
    public int Clicks { get; set; }
}