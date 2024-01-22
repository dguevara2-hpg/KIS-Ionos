using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class Analytics_Search_Term
{
    public required string Term { get; set; }

    public int Searches { get; set; }
}