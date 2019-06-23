using AspNetIdentiy.Infrastructure;
using AspNetIdentiy.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AspNetIdentiy
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(c =>
            {
                c.UseSqlServer(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AppIdentityDbContext;Data Source=.");
            });


            //services.AddIdentity<AppUser, AppRole>( Step 4 : UserId change from guid to int
            services.AddIdentity<AppUser, IdentityRole>(
                config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequiredLength = 6;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddDefaultTokenProviders(); // توکن برای لاگین دو مرحله ایی

            services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordValidator>();
            services.AddTransient<IUserValidator<AppUser>, CustomUserValidator>();

            services.AddMvc();

            services.ConfigureApplicationCookie(c => {
                c.AccessDeniedPath = new PathString("/Account/AccessDenied");//صفحه دسترسی ندارید
                //c.CookieDomain = "" وقتی دامنه لاگین با دامنه خود اپ متفاوت است
                c.LoginPath = new PathString("/Account/Index");
            });

            services.AddAuthorization(c =>
            {
                c.AddPolicy("Check_RegistrationDateClaim", d =>
                {
                    d.RequireAuthenticatedUser(); //برای کاربری که لاگین است
                    d.RequireRole("Admin"); // برای کاربرانی که رل ادمین دارند

                    //d.RequireClaim("RegistrationDateClaim"); //کاربرانی که این کلیم را دارند با هر مقداری
                    //d.RequireClaim("RegistrationDateClaim", "1396-01-01", "1396-02-01" ,""); // آنهاییکه علاوه بر شروط بالا کلیم مشخص شده با مقادیر "1396-01-01", "1396-02-01" ,"" دارند

                    //d.RequireAssertion(f => { return true}); // در این حالت یک تابع ورودی می گیرد
                    d.RequireAssertion(x =>
                    {
                        var claimValue = x.User.Claims.First(e => e.Type == "RegistrationDateClaim").Value;
                        var result = DateTime.Now - DateTime.Parse(claimValue);
                        if (result.Days > 90)
                        {
                            return true;
                        }
                        return false;
                    });
                });
                c.AddPolicy("Check_EditRequirment", d =>
                {
                    //هر کاربر فقط داده ای که خود اضافه کرده ویرایش کند
                    d.Requirements.Add(new CustomeRequirment());
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseAuthentication(); //before Use Mvc

            app.UseMvcWithDefaultRoute();
        }
    }
}