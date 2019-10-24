using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpiderLanguage.Data;
using SpiderLanguage.Middleware;
using SpiderLanguage.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;

namespace SpiderLanguage
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {

                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<SpiderLanguageContext>();
            services.AddDbContext<SpiderLanguageContext>(options =>
            options.UseSqlite("Data Source = spiderLanguage.db"));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireEmail", policy => policy.RequireClaim(ClaimTypes.Email));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SpiderLanguageContext spiderLanguageContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            spiderLanguageContext.Database.EnsureDeleted();
            spiderLanguageContext.Database.EnsureCreated();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseNodeModules(env.ContentRootPath);
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "SpiderLanguageRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" },
                    constraints: new { id = "[0-9]+" });
            });
        }
    }
}
