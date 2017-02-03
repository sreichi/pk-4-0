using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Reichinger.Masterarbeit.PK_4_0.Test
{
    public class MyLoggingClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.RequestUri.ToString());
            HttpResponseMessage result = await base.SendAsync(request, cancellationToken);
            Console.WriteLine(result.StatusCode);
            return result;
        }
    }
    public class DatabaseFixture : IDisposable
    {
        private IWebHost _server;
        private HttpClient _client;
        private readonly string _baseUri = "http://localhost:8001";
        public IntegrationClient IClient { get; set; }


        public DatabaseFixture()
        {
            var builder = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(_baseUri)
                .UseStartup<StartupTestEnvironment>();

            _server = builder.Build();

            try
            {
                Task.Run(() => _server.Run());
            }
            catch (Exception)
            {
                // ignored
            }

            IClient = IntegrationClient.Create("testclient", "test", "api");
            var tokeresponse = IClient.GetTokenResponseForClient().Result;

            _client = new HttpClient()
            {
                BaseAddress = new Uri(_baseUri),
            };
            _client.SetBearerToken(tokeresponse.AccessToken);
        }

        public async Task<HttpResponseMessage> GetHttpResult(string path)
        {
            return await _client.GetAsync(path);
        }


        public async Task<HttpResponseMessage> PostHttpResult(string urlPath, string json)
        {
            return await _client.PostAsync(urlPath, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> DeleteHttpResult(string urlPath)
        {
            return await _client.DeleteAsync(urlPath);
        }

        public async Task<HttpResponseMessage> PutHttpResult(string urlPath, string json)
        {
            return await _client.PutAsync(urlPath, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public void Dispose()
        {
            _server.Dispose();
            _client.Dispose();

            Console.WriteLine("End");
        }
    }
}