using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Areas.User.Models
{
    public class UserProfile : IdentityUser
    {
        public string LastName { get; set; }
    }
}
