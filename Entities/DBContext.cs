using Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class DBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    }
}
