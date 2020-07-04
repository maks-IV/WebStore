using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Areas.Manage.Views.Profile
{
    public static class ManageNavPages
    {
        public static string Profile => "Profile";

        public static string Email => "Email";

        public static string Password => "Password";

        public static string PersonalData => "PersonalData";

        public static string ProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, Profile);

        public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        public static string PassswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, Password);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        public static string PageNavClass(ViewContext context, string page)
        {
            var activePage = context.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(context.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
