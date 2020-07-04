using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Areas.Seller.Models
{
    public class SellerContext : IdentityDbContext<UserSeller>
    {
        public SellerContext(DbContextOptions<SellerContext> options)
           : base(options)
        {
            Database.Migrate();
        }
    }
}
