using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class RequestActivity
{
    public decimal ActivityId { get; set; }

    public string? UGuid { get; set; }

    public decimal? RoleId { get; set; }

    public decimal? RequestStatusId { get; set; }

    public int? FacilityId { get; set; }

    public string? EmailType { get; set; }

    public string? UserGuid { get; set; }

    public string? InternalNote { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual ProviderIdn? Facility { get; set; }

    public virtual RequestStatus? RequestStatus { get; set; }

    public virtual UserRole? Role { get; set; }
}
