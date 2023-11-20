using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace FisAdmin
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static readonly ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineStyle("admin-fis")
                .SetUrl("~/FisAdmin/css/FisAdmin.min.css", "~/FisAdmin/css/FisAdmin.css")
                .SetVersion("1.0.0");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
