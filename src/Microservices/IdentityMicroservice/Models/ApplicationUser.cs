﻿using Microsoft.AspNetCore.Identity;

namespace IdentityMicroservice.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool UsedExternalProvider { get; set; }
    }
}
