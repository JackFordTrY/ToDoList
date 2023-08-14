using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;

namespace ToDoList.WebUI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options => options.AddPolicy("DefaultPolicy", policy =>
                policy.WithOrigins("http://localhost:4200")
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = "/api/users/login";
            options.Cookie.Name = "todolist-session";
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.None;
        });

        services.AddAuthorization();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
