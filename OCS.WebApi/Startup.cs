using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OCS.DAL.EF.Context;
using OCS.WebApi.Attributes;
using OCS.WebApi.Extensions;
using OCS.WebApi.SignalR.Hubs.Chats;

namespace OCS.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("All", b =>
                b.WithOrigins("http://localhost:1570", "https://localhost:5005", "http://localhost:5004")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
            ));

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidationFilterAttribute));
            }).AddFluentValidation();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwagger();

            services.AddSignalR();

            var authConfig = services.AddAuthenticationConfiguration(Configuration);

            services.AddIdentityAuthorization(authConfig);

            var dbConfig = services.AddDatabaseConfiguration(Configuration);

            services.ConfigureDatabase<OnlineChatServiceDbContext>(dbConfig);

            services.RegisterRepositories();

            services.RegisterServices();

            services.RegisterMapperProfiles();

            services.RegisterValidators();

            services.RegisterSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("All");

            app.UseFileServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatApiDescription");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatMessageHub>("/signalr/chats");
            });
        }
    }
}