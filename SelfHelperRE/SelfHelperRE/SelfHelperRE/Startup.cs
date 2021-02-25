using AutoMapper;
using DbModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Mapping;
using SelfHelperRE.ViewModels;
using SelfHelperRE.Models;

namespace SelfHelperRE
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                 {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingDiary<DiaryCatch>());
                mc.AddProfile(new MappingNote<NoteCatch>());
                mc.AddProfile(new MappingTarget<TargetCatch>());
                mc.AddProfile(new MappingUser<CheckEmail>());
                mc.AddProfile(new MappingUser<LoginModel>());
                mc.AddProfile(new MappingUser<RegisterModel>());
                mc.AddProfile(new MappingUser<UserData>());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddTransient<IDiary<Diary>, WorkingWithDiary>();
            services.AddTransient<INote<Note>, WorkingWithNote>();
            services.AddTransient<ITarget<Target>, WorkingWithTarget>();
            services.AddTransient<IUser<User>, WorkingWithUser>();
            
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
