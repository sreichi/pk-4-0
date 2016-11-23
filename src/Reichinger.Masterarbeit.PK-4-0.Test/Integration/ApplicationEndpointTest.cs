using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using FluentAssertions;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    public class ApplicationEndpointTest : IntegrationTestBase
    {
        private const string UrlPath = "/applications/";
        private const int ApplicationId = 1;
        private const int InvalidApplicationId = 987654;

        [Fact]
        public async void GettAllApplicationsShouldReturnAListOfApplicationDtos()
        {
            var result = await GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var applications = JsonConvert.DeserializeObject<List<ApplicationDto>>(result.Content.ReadAsStringAsync().Result);
            applications.Count.Should().Be(2);
            applications.ForEach(dto => dto.Should().BeOfType<ApplicationDto>());
        }

        [Fact]
        public async void GetApplicationByIdShouldReturnOneElement()
        {
            var result = await GetHttpResult(UrlPath + ApplicationId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var application = JsonConvert.DeserializeObject<ApplicationDto>(result.Content.ReadAsStringAsync().Result);
            application.Should().BeOfType<ApplicationDto>();
        }

        [Fact]
        public async void GetApplicationByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await GetHttpResult(UrlPath + InvalidApplicationId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}