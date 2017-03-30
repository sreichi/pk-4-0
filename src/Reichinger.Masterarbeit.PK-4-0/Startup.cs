using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure.ApplicationModelProvider;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure.Identity;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Reichinger.Masterarbeit.PK_4_0.Repositories;
using Swashbuckle.Swagger.Model;

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
            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            var identitySrvConnectionString = Configuration["DbContextSettings:IdentitySrvConnectionString"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;


            //add all repositories to the IoC container.
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IConferenceRepository, ConferenceRepository>();
            services.AddTransient<IFieldTypeRepository, FieldTypeRepository>();
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IStyleRepository, StyleRepository>();
            services.AddTransient<IValidationRepository, ValidationRepository>();
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            services.AddTransient<IApplicationModelProvider, RemoveAuthorizationModelProvider>();


            services.AddCors();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddConfigurationStore(builder => builder.UseNpgsql(identitySrvConnectionString,
                    options => options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder => builder.UseNpgsql(identitySrvConnectionString,
                    options => options.MigrationsAssembly(migrationsAssembly)))
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();

            // Add framework services.
            services.AddMvc();


            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "API Schnittstelle für die Prüfungskommision der Hochschule Augsburg",
                    Description =
                        "Hier sind alle Routen aufgelistet die zur verfügung stehen. Zuvor muss jedoch ein JWT Token über" +
                        "den Authorize Button hinzugefügt werden",
                    TermsOfService = "Some Terms"
                });

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var xmlPath =
                    $"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_environment.ApplicationName}.xml";
                options.IncludeXmlComments(xmlPath);
            });


            services.AddDbContext<ApplicationDbContext>(
                opts => opts.UseNpgsql(connectionString)
            );



            PolicyCreator.CreatePolicies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var authorityUrl = Configuration["IdentityServer:AuthorityUrl"];

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseIdentityServer();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = authorityUrl,
                RequireHttpsMetadata = false,
                ApiName = "api"
            });

            app.UseMvc();

            app.UseSwagger((httpRequest, swaggerDoc) => { swaggerDoc.Host = httpRequest.Host.Value; });

            app.UseSwaggerUi();

            if (_environment.IsEnvironment("Development") || _environment.IsEnvironment("Travis"))
            {
                // if uncommenting this the application is clearing and seeding the database on every start.
                // if you want to work with peristent data for longer time keep this comment

                // app.SeedData();
            }

            IdentityServerStorageSeed.InitializeIdentitySrvDatabase(app);
        }
    }
}