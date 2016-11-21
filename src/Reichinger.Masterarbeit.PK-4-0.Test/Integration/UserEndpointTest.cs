using System.Net;
using FluentAssertions;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    public class UserEndpointTest: IntegrationTestBase
    {
        private const string UrlPath = "/users/";

        [Fact]
        public async void GetAllUsersShouldReturnStatusCodeOk()
        {
            var result = await GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void GetUserByIdShouldReturnOneUser()
        {
            var result = await GetHttpResult(UrlPath + 1);
            result.Content.ReadAsStringAsync();
        }
    }
}