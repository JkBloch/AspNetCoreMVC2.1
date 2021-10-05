using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JkBook.Database;
using JkBook.Models;
using JkBook.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using JkBook.Helpers;
using JkBook.Service;

namespace JkBook
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<BookRepository, BookRepository>();
            //services.AddScoped<LanguageRepository, LanguageRepository>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => 
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.DefaultLockoutTimeSpan=  TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 3;


            }
                );

            services.Configure<DataProtectionTokenProviderOptions>(option =>
                {
                    option.TokenLifespan = TimeSpan.FromMinutes(20);
                });

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = _configuration["Application:LoginPath"];
            });

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IHRRepository, HRRepository>();

            services.AddSingleton<IMessageRepository, MessageRepository>();
            

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            services.Configure<SMTPConfigModel>( _configuration.GetSection("SMTPConfig"));

            services.Configure<NewBookAlertConfig>("InternalBook",_configuration.GetSection("NewBookAlert"));
            services.Configure<NewBookAlertConfig>("ThirdPartyBook",_configuration.GetSection("ThirdPartyBookAlert"));

            services.AddMvc();

            //services.AddMvc().AddViewOptions(options =>
            //#if DEBUG
            //           options.HtmlHelperOptions.ClientValidationEnabled = true                  
            //#endif

            //);
#if DEBUG
            //services.AddMvc().AddViewOptions(options => 
            //options.HtmlHelperOptions.ClientValidationEnabled = false 
            //);
            // Enable or Disable Client Side Validation at Application Level
            //HtmlHelperOptions.ClientValidationEnabled = true;
            //HtmlHelper.UnobtrusiveJavaScriptEnabled = true;

            //services.Configure<HtmlHelperOptions>( x => x.ClientValidationEnabled = false);
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
               
                app.UseDeveloperExceptionPage();
            }


            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                //routes.MapRoute(
                //    name: "MyArea",
                //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync(env.EnvironmentName);
            //    await context.Response.WriteAsync(env.EnvironmentName);

            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from my second middleware");
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from my third middleware");
            //    await next();
            //});
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World HRU hi!");
            //});
        }
    }
}
