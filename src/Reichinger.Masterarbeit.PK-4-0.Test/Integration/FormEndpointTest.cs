using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    public class FormEndpointTest : IntegrationTestBase
    {
        private const string UrlPath = "/forms/";
        private const int FormId = 1;
        private const int InvalidFormId = 98765;

        [Fact]
        public async void GetAllFormsShouldReturnAListofFormDtos()
        {
            var result = await GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var forms = JsonConvert.DeserializeObject<List<FormDto>>(result.Content.ReadAsStringAsync().Result);
            forms.ForEach(form => form.Should().BeOfType<FormDto>());
            forms.Count.Should().Be(2);
        }

        [Fact]
        public async void GetFormByIdShouldReturnOneFormDto()
        {
            var result = await GetHttpResult(UrlPath + FormId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var form = JsonConvert.DeserializeObject<FormDto>(result.Content.ReadAsStringAsync().Result);
            form.Should().BeOfType<FormDto>();
        }

        [Fact]
        public async void GetFormByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await GetHttpResult(UrlPath + InvalidFormId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}