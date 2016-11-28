using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Test
{
    public class DatabaseFixture : IDisposable
    {
//        private IHostingEnvironment _environment;

        private TestServer _server;
        private HttpClient _client;

        public DatabaseFixture()
        {
            Console.WriteLine("Start");

//            _environment = environment;
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(environment.ContentRootPath)
//                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
//                .AddEnvironmentVariables();
//            Configuration = builder.Build();
//
//
//            var connectionString = Configuration["DbContextSettings:ConnectionString"];
//            services.AddDbContext<ApplicationDbContext>(
//                opts => opts.UseNpgsql(connectionString)
//            );

            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();

//            app.SeedData();

        }

        public async Task<HttpResponseMessage> GetHttpResult(string path)
        {
            return await _client.GetAsync(path);
        }

        public IConfigurationRoot Configuration { get; set; }

        public void Dispose()
        {
            Console.WriteLine("Finish");
        }
    }
}