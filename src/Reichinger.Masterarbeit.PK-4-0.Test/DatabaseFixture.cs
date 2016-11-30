using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Reichinger.Masterarbeit.PK_4_0.Test
{
    public class DatabaseFixture : IDisposable
    {

        private TestServer _server;
        private HttpClient _client;

        public DatabaseFixture()
        {
            Console.WriteLine("Start");

            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();

        }

        public async Task<HttpResponseMessage> GetHttpResult(string path)
        {
            return await _client.GetAsync(path);
        }


        public async Task<HttpResponseMessage> PostHttpResult(string urlPath, string json)
        {
            return await _client.PostAsync(urlPath, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public IConfigurationRoot Configuration { get; set; }

        public void Dispose()
        {
            _server.Dispose();
            _client.Dispose();

            Console.WriteLine("End");
        }
    }
}