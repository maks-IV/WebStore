using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using WebStore.Areas.User.Models;

namespace WebStore.Areas.Manage.ViewModels
{
    public class IndexViewModel
    {
        public string UserName { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

    }
}
