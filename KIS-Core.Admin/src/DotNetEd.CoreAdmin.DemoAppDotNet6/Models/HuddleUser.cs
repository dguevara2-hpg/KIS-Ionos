using System;
using System.Collections.Generic;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class HuddleUser
{
    public string? Id { get; set; }

    public string? SsoIdentifier { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Email { get; set; }

    public string? JobTitle { get; set; }

    public string? Idn { get; set; }
}
