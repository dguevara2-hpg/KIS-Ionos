using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int FeedbackTypeId { get; set; }

    public string? DocumentId { get; set; }

    public string? Selection { get; set; }

    public string? Feedback1 { get; set; }

    public string? Userid { get; set; }

    public string? SessionId { get; set; }

    public int? SessionCount { get; set; }

    public DateTime Tstamp { get; set; }

    public virtual FeedbackType FeedbackType { get; set; } = null!;
}
