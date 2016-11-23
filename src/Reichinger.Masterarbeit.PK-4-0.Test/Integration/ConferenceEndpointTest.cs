using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Http;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    public class ConferenceEndpointTest : IntegrationTestBase
    {
        private const string UrlPath = "/conferences/";
        private const int ConferenceId = 1;
        private const int InvalidConferenceId = 9876543;


        [Fact]
        public async void GetAllConferencesShouldReturnAListOfConferenceDtos()
        {
            var result = await GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var conferences = JsonConvert.DeserializeObject<List<ConferenceDto>>(result.Content.ReadAsStringAsync().Result);
            conferences.ForEach(conference => conference.Should().BeOfType<ConferenceDto>());
        }

        [Fact]
        public async void GetConferenceByIdShouldReturnOneConferenceDto()
        {
            var result = await GetHttpResult(UrlPath + ConferenceId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var confernce = JsonConvert.DeserializeObject<ConferenceDto>(result.Content.ReadAsStringAsync().Result);
            confernce.Should().BeOfType<ConferenceDto>();
        }

        [Fact]
        public async void GetConferenceByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await GetHttpResult(UrlPath + InvalidConferenceId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}