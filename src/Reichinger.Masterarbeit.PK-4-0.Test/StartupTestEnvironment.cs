using System;
using System.IO;
using System.Reflection;
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

namespace Reichinger.Masterarbeit.PK_4_0.Test
{
    public class StartupTestEnvironment
    {
        private readonly IHostingEnvironment _environment;

        public StartupTestEnvironment(IHostingEnvironment environment)
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

            services.AddDbContext<ApplicationDbContext>(
                opts => opts.UseNpgsql(connectionString)
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseIdentityServer();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:8001",
                RequireHttpsMetadata = false,
                ApiName = "api"
            });

            app.UseMvc();

            if (_environment.IsEnvironment("Development") || _environment.IsEnvironment("Travis"))
            {
                app.SeedData();
            }

            IdentityServerStorageSeed.InitializeIdentitySrvDatabase(app);
        }
    }
}