using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    [Collection("Test collection")]
    public class ConfigurationEndpointTest
    {
        private readonly DatabaseFixture _fixture;
        private readonly string UrlPath = "/config/";

        public ConfigurationEndpointTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetFieldDefinitionsShouldReturnAListOfDtos()
        {
            var result = await _fixture.GetHttpResult(UrlPath+"fieldDefinitions");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var conferences = JsonConvert.DeserializeObject<List<FieldDefinitionDto>>(result.Content.ReadAsStringAsync().Result);
            conferences.ForEach(conference => conference.Should().BeOfType<FieldDefinitionDto>());
        }

        [Fact]
        public async void GetStylesShouldReturnAListOfDtos()
        {
            var result = await _fixture.GetHttpResult(UrlPath+"styles");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var conferences = JsonConvert.DeserializeObject<List<StyleDto>>(result.Content.ReadAsStringAsync().Result);
            conferences.ForEach(conference => conference.Should().BeOfType<StyleDto>());
        }

        [Fact]
        public async void GetValidationsShouldReturnAListOfDtos()
        {
            var result = await _fixture.GetHttpResult(UrlPath+"validations");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var conferences = JsonConvert.DeserializeObject<List<ValidationDto>>(result.Content.ReadAsStringAsync().Result);
            conferences.ForEach(conference => conference.Should().BeOfType<ValidationDto>());
        }
    }
}