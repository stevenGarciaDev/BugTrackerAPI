using AutoMapper;
using BugTrackerAPI.Data;
using BugTrackerAPI.Entities;
using BugTrackerAPI.Helpers;
using BugTrackerAPI.Interfaces;
using BugTrackerAPI.Middleware;
using BugTrackerAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
            })
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddEntityFrameworkStores<BugTrackerDbContext>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("Manager", policy => policy.RequireRole("Admin", "Project Manager"));
                opt.AddPolicy("Member", policy => policy.RequireRole("Admin", "Project Manager", "Developer"));
            });

            //Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<BugTrackerDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("BugTrackerContextProd"));
                });
            }
            else
            {
                services.AddDbContext<BugTrackerDbContext>(options =>
                {
                    options.UseNpgsql(Configuration.GetConnectionString("BugTrackerPostgresContextDev"));
                    //options.UseSqlServer(Configuration.GetConnectionString("BugTrackerContextDev"));
                });
            }

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            string clientOrigin = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production" ?
                "ClientOrigin" : "ClientOriginDev";

            app.UseCors(policy => policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins(Configuration[clientOrigin]));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
