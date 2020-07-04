using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Areas.User.Models
{
    public class UserContext : IdentityDbContext<UserProfile>
    {
        public UserContext(DbContextOptions<UserContext> options)
           : base(options)
        {
            Database.Migrate();
        }
    }
}
