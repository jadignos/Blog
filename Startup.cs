using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.Configurations;
using Blog.Data.Managers;
using Blog.Data.Repositories;
using Blog.Data.Services;
using Blog.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SmtpSettings>(_config.GetSection("SmtpSettings"));
            services.Configure<MailAccountSettings>(_config.GetSection("MailAccountSettings"));

            services.AddDbContext<AppDbContext>(option =>
                option.UseSqlServer(_config.GetConnectionString("BlogDbConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(setup =>
            {
                setup.Password.RequireDigit = true;
                setup.Password.RequireNonAlphanumeric = true;
                setup.Password.RequireUppercase = true;
                setup.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(configure => configure.LoginPath = "/Auth/Login");

            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ISubCommentRepository, SubCommentRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IImageManager, ImageManager>();

            services.AddSingleton<IMailService, MailService>();

            services.AddMvc(option => option.EnableEndpointRouting = false);
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
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
