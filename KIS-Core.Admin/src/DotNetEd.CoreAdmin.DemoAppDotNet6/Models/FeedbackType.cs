using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class FeedbackType
{
    public int FeedbackTypeId { get; set; }

    public string? FeedbackTypeDescription { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();
}
