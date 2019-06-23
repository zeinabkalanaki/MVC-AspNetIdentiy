using System.Threading.Tasks;
using AspNetIdentiy.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentiy.Infrastructure
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public override Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            return base.ValidateAsync(manager, user);
        }
    }
}
