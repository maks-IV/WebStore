using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Areas.Seller.Models
{
    public class UserSeller : IdentityUser
    {
        public string LastName { get; set; }
    }
}
