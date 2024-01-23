using Entities;
using Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityMVCWebApp.ServicesExtension
{
    public static class ConfigureExtension
    {
        public static void ServicesExtension(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DBContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<DBContext>().
                AddUserStore<UserStore<ApplicationUser, ApplicationRole,DBContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, DBContext, Guid>>();


            services.AddAuthorization(
                options => { options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); }
            );

            services.ConfigureApplicationCookie(options => { options.LoginPath = "/UserAccess/Login"; });
        }
    }
}
