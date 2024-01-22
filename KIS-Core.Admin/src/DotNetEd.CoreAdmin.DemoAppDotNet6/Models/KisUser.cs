using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class KisUser
{
    public string Guid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public byte RoleId { get; set; }

    public byte RequestStatusId { get; set; }

    public int? HuddleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public byte? FacilityId { get; set; }

    public string? FacilityIdn { get; set; }

    public string? JobTitle { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string? Password { get; set; }

    public string? ResetPasswordKey { get; set; }

    public string? ResetPasswordTstamp { get; set; }

    public DateTime RequestedDate { get; set; }

    public bool IsApproved { get; set; }

    public string? ApprovedBy { get; set; }

    public string? ApprovedDate { get; set; }

    public byte RoleType { get; set; }

    public bool DevelopmentAccess { get; set; }

    public bool StagingAccess { get; set; }

    public byte ProductionAccess { get; set; }
}
