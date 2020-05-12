using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OCS.BLL.Configurations.MapperProfiles;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.DTOs.Users;
using OCS.BLL.Services.Chats.Group;
using OCS.BLL.Services.Chats.Private;
using OCS.BLL.Services.Contracts.Chats.Group;
using OCS.BLL.Services.Contracts.Chats.Private;
using OCS.BLL.Services.Contracts.Users;
using OCS.BLL.Services.Users;
using OCS.DAL.EF.UnitOfWorks;
using OCS.DAL.UnitOfWorks.Contracts;
using OCS.WebApi.Configurations;
using OCS.WebApi.SignalR.Hubs.Chats.Configurations;
using OCS.WebApi.SignalR.Hubs.Chats.Configurations.Contracts;
using OCS.WebApi.SignalR.Hubs.Configurations;
using OCS.WebApi.SignalR.Hubs.Configurations.Contracts;
using OCS.WebApi.SignalR.PrincipalProviders;
using OCS.WebApi.Validations.Chats.Groups;
using OCS.WebApi.Validations.Chats.Privates;
using OCS.WebApi.Validations.Users;
using System.Collections.Generic;
using System.Reflection;

namespace OCS.WebApi.Extensions
{

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, OnlineChatServiceUnitOfWork>();
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserContactService, UserContactService>();
            services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();

            services.AddScoped<IGroupChatMessageService, GroupChatMessageService>();
            services.AddScoped<IGroupChatService, GroupChatService>();
            services.AddScoped<IPrivateChatMessageService, PrivateChatMessageService>();
            services.AddScoped<IPrivateChatService, PrivateChatService>();

            return services;
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<UserLoginDto>, UserLoginValidator>();
            services.AddSingleton<IValidator<UserRegistrationDto>, UserRegistrationValidator>();
            services.AddSingleton<IValidator<AddUserToContactDto>, AddUserToContactValidator>();

            services.AddSingleton<IValidator<AddUserToGroupChatDto>, AddUserToGroupChatValidator>();
            services.AddSingleton<IValidator<CreateGroupChatDto>, CreateGroupChatValidator>();
            services.AddSingleton<IValidator<CreateGroupChatMessageDto>, CreateGroupChatMessageValidator>();

            services.AddSingleton<IValidator<CreatePrivateChatDto>, CreatePrivateChatValidator>();
            services.AddSingleton<IValidator<CreatePrivateChatMessageDto>, CreatePrivateChatMessageValidator>();

            return services;
        }

        public static IServiceCollection RegisterMapperProfiles(this IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<BllMapperProfile>();
            });

            services.AddSingleton(c => config.CreateMapper());

            return services;
        }

        public static IServiceCollection RegisterSignalR(this IServiceCollection services)
        {
            services.AddSingleton<IUserIdProvider, ChatUserIdProvider>();

            services.AddScoped<ISignalRBaseConfiguration, SignalRBaseConfiguration>();
            services.AddScoped<ISignalRChatConfiguration, SignalRChatConfiguration>();

            return services;
        }

        public static DatabaseConfiguration AddDatabaseConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new DatabaseConfiguration();

            configuration.Bind("DatabaseConfiguration", config);
            services.AddSingleton(config);

            return config;
        }

        public static IServiceCollection ConfigureDatabase<TContext>(
            this IServiceCollection services, DatabaseConfiguration csConfig)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(csConfig.ConnectionString));
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Assembly.GetExecutingAssembly().GetName().Name, Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });
            });

            return services;
        }
    }
}