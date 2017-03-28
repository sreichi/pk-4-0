using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity
{
    public static class PolicyCreator
    {
        public static void CreatePolicies(IServiceCollection services)
        {
            var serviceProvider = new DefaultServiceProviderFactory().CreateServiceProvider(services);
            var permissionRepository = serviceProvider.GetService<IPermissionRepository>();

            var allPermissions = permissionRepository.GetAllPermissions();

            services.AddAuthorization(options =>
            {
                foreach (var permission in allPermissions)
                {
                    options.AddPolicy($"policy{permission.Name}", user =>
                    {
                        user.RequireClaim("permission", permission.Name);
                    });
                }
            });
        }
    }
}