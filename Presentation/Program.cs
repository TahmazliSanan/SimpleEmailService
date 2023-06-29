using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities.EmailEntity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Services.Abstracts;
using Services.Concretes;
using Services.Configs;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseNpgsql(
                    builder.Configuration.GetConnectionString("Connection")
                    )
                );

            var mapperConfig = new MapperConfiguration(
                mc => mc.AddProfile(
                    new MapperProfile()
                    )
                );
            builder.Services.AddSingleton(mapperConfig.CreateMapper());

            var emailConfig = builder.Configuration.GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(auth =>
                {
                    auth.Cookie.HttpOnly = true;
                    auth.Cookie.Name = "AuthCookie";
                    auth.Cookie.MaxAge = TimeSpan.FromHours(24);
                    auth.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = x =>
                        {
                            x.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}