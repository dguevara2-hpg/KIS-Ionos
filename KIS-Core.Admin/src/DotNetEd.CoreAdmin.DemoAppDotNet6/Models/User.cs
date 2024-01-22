using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class User
{
    public string Guid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public decimal? RoleId { get; set; }

    public decimal? RequestStatusId { get; set; }

    public decimal? HuddleId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? FacilityId { get; set; }

    public string? FacilityIdn { get; set; }

    public string? JobTitle { get; set; }

    public string? EmailAddress { get; set; }

    public string? Password { get; set; }

    public string? ResetPasswordKey { get; set; }

    public DateTime? ResetPasswordTstamp { get; set; }

    public DateTime? RequestedDate { get; set; }

    public int IsApproved { get; set; }

    public string? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public virtual ProviderIdn? Facility { get; set; }

    public virtual RequestStatus? RequestStatus { get; set; }

    public virtual UserRole? Role { get; set; }
}
