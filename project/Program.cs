using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using project.Controllers;
using project.Models;
using project.Repository;

namespace project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //public void configure
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(
                Options => Options.IdleTimeout = TimeSpan.FromMinutes(10));

                builder.Services.AddDbContext<ItiContext>(     //builtin services which is not already registered like this >> always start with add word   // when ask for dbcontext >> provide with obj from iticontext >> using optionsBuilder >> make config..>> send options >> to iticontext constr.. which take options and send them to base >> congrats u have obj from iticontext            // using these services >> no need to make obj from iticontext or repo.. >> i ask for them and services "ioc container" will give me these obj "injection"
                Options_builder => Options_builder.UseSqlServer(builder
                .Configuration.GetConnectionString("c.s"))
                );

            builder.Services.AddScoped<IcourseRepository, CourseRepository>();   // services >> ioc container 
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();  // built in service >> i tell him when ask for iinustructor repository >> send obj from instructor rep.. >> add scoped to tell him life cycle for obj >> but above in adddbcontext he knows that the asking will be for dbcontext >> i just tell him obj from which class will be send "built in and i register" 

            builder.Services.AddIdentity<AppUser, IdentityRole>(
                options => { options.Password.RequireNonAlphanumeric = true; })
                .AddEntityFrameworkStores<ItiContext>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(     // pattern >> help seo , validation on url before going to e.p , dynamic routing based on placeholder how "always 1 e.p" ????????
/**/                name: "route1",     // use of name "if this dic >> where key and val >> 3 parts >> name , pattern , end point"??????
/**/                pattern: "l1",      // benefit >> always i reach e.p via button so cont.. and action will be written in url not this pattern i will never use url >> so never pattern appear????????
                new { controller = "account", action = "login" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=index}/{id?}");

            app.Run();
        }
    }
}
