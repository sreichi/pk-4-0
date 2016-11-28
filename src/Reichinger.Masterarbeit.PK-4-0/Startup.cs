using System;
using System.Collections;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
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
            //add all repositories to the IoC container.
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IConferenceRepository, ConferenceRepository>();
            services.AddTransient<IFieldTypeRepository, FieldTypeRepository>();
            services.AddTransient<IFieldRepository, FieldRepository>();
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();


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

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath + "\\Reichinger.Masterarbeit.PK-4-0.xml");
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

            app.UseMvc();

            app.UseSwagger((httpRequest, swaggerDoc) => { swaggerDoc.Host = httpRequest.Host.Value; });

            app.UseSwaggerUi();
//
//            if (_environment.IsEnvironment("Development") || _environment.IsEnvironment("Travis"))
//            {
                app.SeedData();
//            }
        }
    }
}