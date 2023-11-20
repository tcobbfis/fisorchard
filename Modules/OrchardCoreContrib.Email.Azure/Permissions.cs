using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.Azure
{
    /// <summary>
    /// Represents a permissions that will be applied into Gmail mailing module.
    /// </summary>
    public class Permissions : IPermissionProvider
    {
        /// <summary>
        /// Gets a permission for managing a Gmail settings.
        /// </summary>
        public static readonly Permission ManageAzureSettings = new Permission("ManageAzureSettings", "Manage Azure email communicator Settings");

        /// <inheritdoc/>
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageAzureSettings
            }
            .AsEnumerable());
        }

        /// <inheritdoc/>
        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageAzureSettings }
                },
            };
        }
    }
}
