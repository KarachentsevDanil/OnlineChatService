using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OCS.BLL.Configurations;
using OCS.DAL.EF.Context;
using OCS.DAL.Entities.Users;
using System.Text;
using System.Threading.Tasks;

namespace OCS.WebApi.Extensions
{

    public static class AuthenticationExtensions
    {
        private const string JwtQueryParameter = "access_token";

        private const string SignalRBaseSegment = "/signalr";

        public static AuthenticationConfiguration AddAuthenticationConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new AuthenticationConfiguration();

            configuration.Bind("AuthenticationConfiguration", config);
            services.AddSingleton(config);

            return config;
        }

        public static IServiceCollection AddIdentityAuthorization(
            this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<OnlineChatServiceDbContext>()
                .AddDefaultTokenProviders();

            services.AddJwtAuthentication(configuration);

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = configuration.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query[JwtQueryParameter].ToString() ?? context.Request.GetJwtToken();

                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments(SignalRBaseSegment))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        public static SecurityKey GetSymmetricSecurityKey(this AuthenticationConfiguration configuration)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.TokenKey));
        }
    }
}