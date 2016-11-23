using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    public class RoleEndpointTest: IntegrationTestBase
    {
        private const string UrlPath = "/roles/";
        private const int RoleId = 1;
        private const int InvalidRoleId = 987654;

        [Fact]
        public async void GettAllApplicationsShouldReturnAListOfApplicationDtos()
        {
            var result = await GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var applications = JsonConvert.DeserializeObject<List<FormDto>>(result.Content.ReadAsStringAsync().Result);
            applications.Count.Should().Be(2);
            applications.ForEach(dto => dto.Should().BeOfType<FormDto>());
        }

        [Fact]
        public async void GetApplicationByIdShouldReturnOneElement()
        {
            var result = await GetHttpResult(UrlPath + RoleId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var application = JsonConvert.DeserializeObject<FormDto>(result.Content.ReadAsStringAsync().Result);
            application.Should().BeOfType<FormDto>();
        }

        [Fact]
        public async void GetApplicationByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await GetHttpResult(UrlPath + InvalidRoleId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}