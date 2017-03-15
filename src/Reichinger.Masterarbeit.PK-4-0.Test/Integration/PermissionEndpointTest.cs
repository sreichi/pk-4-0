using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{

    [Collection("Test collection")]
    public class PermissionEndpointTest
    {
        private readonly DatabaseFixture _fixture;
        private readonly string UrlPath = "/permissions/";

        public PermissionEndpointTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetPermissionsShouldReturnAListOfPermissionDtos()
        {
            var result = await _fixture.GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var permissions = JsonConvert.DeserializeObject<List<PermissionDto>>(result.Content.ReadAsStringAsync().Result);
            permissions.ForEach(conference => conference.Should().BeOfType<PermissionDto>());
        }

    }
}