using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class Document
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public string? Link { get; set; }

    public string? InsightCategory { get; set; }

    public string? DocumentTitle { get; set; }

    public string? Department { get; set; }

    public DateTime? Modified { get; set; }

    public string? ModifiedBy { get; set; }

    public string? ContractNumber { get; set; }

    public string? Specialty { get; set; }

    public string? ItemType { get; set; }

    public string? Provisioning { get; set; }

    public string? Path { get; set; }

    public string? Category { get; set; }

    public DateTime? ImportedDate { get; set; }
}
