using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AspNetIdentiy.Models
{
    /*
     * IdentityUser Has:
     * Id
     * Username
     * Claims
     * Email
     * Logins از چند سایت لاگین کرده
     * PasswordHash
     * Role این جزو کلیم ها است اما به خاطر دید سنتی این را هم اینجا گذاشته
     * PhoneNumber
     * SecurityStampres
     */
    public class AppUser : IdentityUser //After change this class need to add-migration
    //public class AppUser : IdentityUser<int> // Step 1 : UserId change from guid to int 
    {
        public DateTime BithDate { get; set; }
        public string DepartmentDate { get; set; }
    }

    public class AppRole: IdentityRole<int> // Step 2 : UserId change from guid to int 
    {

    }

    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    //public class AppIdentityDbContext : IdentityDbContext<AppUser,AppRole , int>  Step 3 : UserId change from guid to int //برای کاستومایز بیشتر باید از اورلود های همین استفاده کرد و پیاده سازی کرد
    {
        public AppIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    //Step 4  in startup : UserId change from guid to int
    //services.AddIdentity<AppUser, AppRole>(
}
