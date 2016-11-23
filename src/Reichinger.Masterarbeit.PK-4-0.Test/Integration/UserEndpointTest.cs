using System.Collections;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Newtonsoft.Json;
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

            var users = JsonConvert.DeserializeObject<List<UserDto>>(result.Content.ReadAsStringAsync().Result);
            users.Count.Should().Be(2);
        }

        [Fact]
        public async void GetUserByIdShouldReturnOneUser()
        {
            var result = await GetHttpResult(UrlPath + 1);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var user = JsonConvert.DeserializeObject<UserDto>(result.Content.ReadAsStringAsync().Result);
            user.Should().BeOfType<UserDto>();
            user.Firstname.Should().Be("Stephan");
        }
    }
}