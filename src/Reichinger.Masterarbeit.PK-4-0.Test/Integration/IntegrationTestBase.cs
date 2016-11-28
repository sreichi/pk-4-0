
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    public class IntegrationTestBase
    {
        private TestServer _server;
        private HttpClient _client;

        public IntegrationTestBase()
        {
            _server = new TestServer(new WebHostBuilder());
            _client = _server.CreateClient();
        }

        protected async Task<HttpResponseMessage> GetHttpResult(string path)
        {
            return await _client.GetAsync(path);
        }
    }
}