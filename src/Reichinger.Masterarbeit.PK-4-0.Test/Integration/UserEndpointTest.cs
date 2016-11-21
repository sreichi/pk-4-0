using System.Net;
using FluentAssertions;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    public class UserEndpointTest: IntegrationTestBase
    {
        private const string UrlPath = "/users";

        [Fact]
        public async void GetAllUsersShouldReturnTwo()
        {
            var result = await GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}