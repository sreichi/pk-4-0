using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    [Collection("Test collection")]
    public class ConferenceEndpointTest
    {
        private readonly DatabaseFixture _fixture;
        private const string UrlPath = "/conferences/";
        private readonly Guid _conferenceId = DataSeeder.ConferenceId1;
        private const int InvalidConferenceId = 9876543;
        private readonly Guid _conferenceToDeleteId = DataSeeder.ConferenceId2;

        public ConferenceEndpointTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetAllConferencesShouldReturnAListOfConferenceDtos()
        {
            var result = await _fixture.GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var conferences = JsonConvert.DeserializeObject<List<ConferenceListDto>>(result.Content.ReadAsStringAsync().Result);
            conferences.ForEach(conference => conference.Should().BeOfType<ConferenceListDto>());
        }

        [Fact]
        public async void GetConferenceByIdShouldReturnOneConferenceDto()
        {
            var result = await _fixture.GetHttpResult(UrlPath + _conferenceId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var confernce = JsonConvert.DeserializeObject<ConferenceListDto>(result.Content.ReadAsStringAsync().Result);
            confernce.Should().BeOfType<ConferenceListDto>();
        }

        [Fact]
        public async void GetConferenceByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await _fixture.GetHttpResult(UrlPath + InvalidConferenceId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void GetApplicationsOfConferenceShouldReturnListOfApplicationsDtos()
        {
            var result = await _fixture.GetHttpResult($"{UrlPath}{_conferenceId}/applications");
            result.Should().NotBeNull();
            var applications =
                JsonConvert.DeserializeObject<List<ApplicationListDto>>(result.Content.ReadAsStringAsync().Result);
            applications.ForEach(application => application.Should().BeOfType<ApplicationListDto>());
        }

        [Fact]
        public async void CreateConferenceShouldReturnOkAndTheNewObject()
        {
            var newConference = new ConferenceCreateDto()
            {
                Description = "New Test Description For Conference",
                DateOfEvent = DateTime.UtcNow,
                StartOfEvent = "15:00",
                EndOfEvent = "18:00",
                RoomOfEvent = "A Room",
                NumberOfConference = 42
            };

            var serializedConference = JsonConvert.SerializeObject(newConference);
            var result = await _fixture.PostHttpResult(UrlPath, serializedConference);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async void CreateConferenceWithInvalidModelStateShouldReturnBadRequest()
        {
            var newConference = new ConferenceCreateDto()
            {
                DateOfEvent = DateTime.Now
            }.ToModel();

            var serializedConference = JsonConvert.SerializeObject(newConference);
            var result = await _fixture.PostHttpResult(UrlPath, serializedConference);


            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void DeleteConferenceByIdShouldReturnOkAndDeleteOneConference()
        {
            var httpResponse = await _fixture.GetHttpResult(UrlPath + _conferenceToDeleteId);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await _fixture.DeleteHttpResult(UrlPath + _conferenceToDeleteId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var httpResponseOfDeletedConference = await _fixture.GetHttpResult(UrlPath + _conferenceToDeleteId);
            httpResponseOfDeletedConference.Should().NotBeNull();
            httpResponseOfDeletedConference.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}