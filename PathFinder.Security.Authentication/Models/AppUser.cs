﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace PathFinder.Security.Authentication.Models
{
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string AvatarPath { get; set; }
    }
}
