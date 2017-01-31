using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Reichinger.Masterarbeit.PK_4_0.Repositories;
using Swashbuckle.Swagger.Model;
using Config = Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity.Config;

namespace Reichinger.Masterarbeit.PK_4_0
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IHostingEnvironment environment)
        {
            _environment = environment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add all repositories to the IoC container.
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IConferenceRepository, ConferenceRepository>();
            services.AddTransient<IFieldTypeRepository, FieldTypeRepository>();
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IStyleRepository, StyleRepository>();
            services.AddTransient<IValidationRepository, ValidationRepository>();
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();

            services.AddCors();


            services.AddIdentityServer()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddTemporarySigningCredential()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();

            // Add framework services.
            services.AddMvc();


            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "PK-4.0 API",
                    Description = "API for the PK-4.0",
                    TermsOfService = "Some terms ..."
                });

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var xmlPath = $"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_environment.ApplicationName}.xml";
                options.IncludeXmlComments(xmlPath);
            });


            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            services.AddDbContext<ApplicationDbContext>(
                opts => opts.UseNpgsql(connectionString)
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseIdentityServer();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:8000",
                RequireHttpsMetadata = false,
                ApiName = "api"
            });

            app.UseMvc();

            app.UseSwagger((httpRequest, swaggerDoc) => { swaggerDoc.Host = httpRequest.Host.Value; });

            app.UseSwaggerUi();

            if (_environment.IsEnvironment("Development") || _environment.IsEnvironment("Travis"))
            {
                app.SeedData();
            }
        }
    }
}