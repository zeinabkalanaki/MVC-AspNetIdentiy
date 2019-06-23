using AspNetIdentiy.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentiy.Infrastructure
{
    /*
    public class CustomPasswordValidator : IPasswordValidator<AppUser> // دیگر سرویس خودش کار نمی کند و چکینگ های خودش عمل نمی کند ، برای اضافه کردن چک جدید باید از کلاس استفاده کرد
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordCountainsUsername",
                    Description = "Password cannot contain username"
                });
            }

            if (password.Contains("12345"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordCountainsSequence",
                    Description = "Password cannot contain Sequence"
                });
            }

            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }

    */

    public class CustomPasswordValidator : PasswordValidator<AppUser>
    {
        public override Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            var baseResult = base.ValidateAsync(manager, user, password).Result;

            List<IdentityError> errors = baseResult.Succeeded ? new List<IdentityError>():baseResult.Errors.ToList();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordCountainsUsername",
                    Description = "Password cannot contain username"
                });
            }

            if (password.Contains("12345"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordCountainsSequence",
                    Description = "Password cannot contain Sequence"
                });
            }

            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));

        }

    }
}
