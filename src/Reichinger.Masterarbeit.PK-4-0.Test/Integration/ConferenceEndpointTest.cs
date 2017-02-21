using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
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
        private readonly Guid _conferenceId3 = DataSeeder.ConferenceId3;
        private readonly Guid _applicationId = DataSeeder.ApplicationId1;
        private const int InvalidApplicationId = 654987;
        private readonly Guid _userId1 = DataSeeder.UserId1;
        private readonly Guid _userId2 = DataSeeder.UserId2;
        private readonly Guid _userId3 = DataSeeder.UserId3;
        private const int InvalidUserId = 987654;

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

        [Fact]
        public async void DeleteConferenceWithInvalidIdShouldReturnNotFound()
        {
            var httpResponse = await _fixture.GetHttpResult(UrlPath + InvalidConferenceId);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void UpdateConferenceByIdShouldReturnTheUpdatedConference()
        {
            var updatedConference = new ConferenceCreateDto()
            {
                DateOfEvent = DateTime.UtcNow,
                Description = "Updated Conference",
                NumberOfConference = 42,
                StartOfEvent = "08:00",
                EndOfEvent = "23:00",
                RoomOfEvent = "L 3.03"
            };

            var serializedConference = JsonConvert.SerializeObject(updatedConference);
            var httpResponse = await _fixture.PutHttpResult(UrlPath + _conferenceId3, serializedConference);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var deserializeObject = JsonConvert.DeserializeObject<ConferenceDetailDto>(httpResponse.Content.ReadAsStringAsync().Result);
            deserializeObject.Should().BeOfType<ConferenceDetailDto>();
        }

        [Fact]
        public async void RemoveApplicationFromConferenceShouldReturnOk()
        {
            var getAllApplicationsOfConferenceHttpResult = await _fixture.GetHttpResult($"{UrlPath}{_conferenceId}/applications");
            var numberOfApplicationsForConference = JsonConvert.DeserializeObject<List<ApplicationListDto>>(getAllApplicationsOfConferenceHttpResult.Content.ReadAsStringAsync()
                .Result).Count;

            var httpResponse = await _fixture.DeleteHttpResult($"{UrlPath}{_conferenceId}/applications/{_applicationId}");
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getAllApplicationsOfConferenceHttpResultAfterRemoving = await _fixture.GetHttpResult($"{UrlPath}{_conferenceId}/applications");
            var numberOfApplicationsForConferenceAfterRemoving = JsonConvert.DeserializeObject<List<ApplicationListDto>>(getAllApplicationsOfConferenceHttpResultAfterRemoving.Content.ReadAsStringAsync()
                .Result).Count;

            numberOfApplicationsForConferenceAfterRemoving.Should().Be(numberOfApplicationsForConference - 1);
        }

        [Fact]
        public async void RemoveApplicationFromConferenceWitInvalidIdShouldReturnOk()
        {
            var httpResponse = await _fixture.DeleteHttpResult($"{UrlPath}{_conferenceId}/applications/{InvalidApplicationId}");
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }



        [Fact]
        public async void RemoveAttendantFromConferenceShouldReturnOk()
        {
            var httpResponse = await _fixture.DeleteHttpResult($"{UrlPath}{_conferenceId}/attendants/{_userId2}");
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void RemoveAttendantWithInvalidIdShouldReturnNotFound()
        {
            var httpResponse = await _fixture.DeleteHttpResult($"{UrlPath}{_conferenceId}/attendants/{InvalidUserId}");
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void AddAttendantToConferenceShouldReturnOk()
        {
            var attendantCreateDto = new AttendantCreateDto()
            {
                UserId = _userId2,
                TypeOfAttendance = TypeOfAttendance.GUEST
            };

            var serializedAssignmentCreateDto = JsonConvert.SerializeObject(attendantCreateDto);
            var httpResponse = await _fixture.PostHttpResult($"{UrlPath}{_conferenceId}/attendants/", serializedAssignmentCreateDto);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async void AddAllreadyExistingAttendantToConferenceShouldReturnBadRequest()
        {
            var attendantCreateDto = new AttendantCreateDto()
            {
                UserId = _userId1,
                TypeOfAttendance = TypeOfAttendance.GUEST
            };

            var serializedAttendantCreateDto = JsonConvert.SerializeObject(attendantCreateDto);
            var httpResponse = await _fixture.PostHttpResult($"{UrlPath}{_conferenceId}/attendants/", serializedAttendantCreateDto);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}