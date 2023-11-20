using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.Users.ViewModels;

namespace Fis.Users.ViewModels;

public class RoleLoginSettingsViewModel
{
    public bool RequireTwoFactorAuthenticationForSpecificRoles { get; set; }

    public RoleEntry[] Roles { get; set; } = Array.Empty<RoleEntry>();
}
