using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class HistorySearch
{
    public long Id { get; set; }

    public string? Value { get; set; }

    public string? Userid { get; set; }

    public string? Page { get; set; }

    public string? SessionId { get; set; }

    public int? SessionCount { get; set; }

    public DateTime Tstamp { get; set; }
}
