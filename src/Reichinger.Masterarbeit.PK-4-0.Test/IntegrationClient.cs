using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Reichinger.Masterarbeit.PK_4_0.Test
{
    public class IntegrationClient
    {
        private static readonly string IdentityServerBaseUri = "http://localhost:8000";
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _scope;

        private IntegrationClient(string clientId, string clientSecret, string scope)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _scope = scope;
        }

        public static IntegrationClient Create(string clientId, string clientSecret, string scope) => new IntegrationClient(clientId, clientSecret, scope);

        public async Task<TokenResponse> GetTokenResponseForClient()
        {
            var disco = await DiscoveryClient.GetAsync(IdentityServerBaseUri);
            var tokenClient = new TokenClient(disco.TokenEndpoint, _clientId, _clientSecret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(_scope);
            return tokenResponse;
        }

        public async Task<TokenResponse> GetTokenResponseForUser(string username, string password)
        {
            var disco = await DiscoveryClient.GetAsync(IdentityServerBaseUri);
            var tokenClient = new TokenClient(disco.TokenEndpoint, _clientId, _clientSecret);
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(username, password, _scope);
            return tokenResponse;
        }

        public async Task<HttpResponseMessage> GetProfileResponseForUser(string username, string password)
        {
            var tokenResponse = await GetTokenResponseForUser(username, password);

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync(IdentityServerBaseUri);

            return response;
        }
    }
}