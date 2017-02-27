using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ApplicationEndpointTest
    {
        private readonly DatabaseFixture _fixture;
        private const string UrlPath = "/applications/";
        private readonly Guid _applicationId = DataSeeder.ApplicationId1;
        private readonly Guid _applicationToUpdateId = DataSeeder.ApplicationId2;
        private readonly Guid _applicationToDelete = DataSeeder.ApplicationId3;
        private readonly Guid _applicationId4 = DataSeeder.ApplicationId4;
        private readonly Guid _userId1 = DataSeeder.UserId1;
        private readonly Guid _userId2 = DataSeeder.UserId2;
        private readonly Guid _userId3 = DataSeeder.UserId3;
        private readonly int _invalidUserIdToUnassign = 543210;
        private const int InvalidApplicationId = 987654;

        public ApplicationEndpointTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GettAllApplicationsShouldReturnAListOfApplicationDtos()
        {
            var result = await _fixture.GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var applications =
                JsonConvert.DeserializeObject<List<ApplicationListDto>>(result.Content.ReadAsStringAsync().Result);
            applications.ForEach(dto => dto.Should().BeOfType<ApplicationListDto>());
        }

        [Fact]
        public async void GetHistoryOfApplicationShouldReturnAListOfApplicationDetailDtos()
        {
            var result = await _fixture.GetHttpResult($"{UrlPath}{_applicationToUpdateId}/history");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var applications =
                JsonConvert.DeserializeObject<List<ApplicationDetailDto>>(result.Content.ReadAsStringAsync().Result);
            applications.ForEach(dto => dto.Should().BeOfType<ApplicationDetailDto>());
        }

        [Fact]
        public async void GetApplicationByIdShouldReturnOneElement()
        {
            var result = await _fixture.GetHttpResult(UrlPath + _applicationId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var application = JsonConvert.DeserializeObject<ApplicationDetailDto>(result.Content.ReadAsStringAsync().Result);
            application.Should().BeOfType<ApplicationDetailDto>();
        }

        [Fact]
        public async void GetApplicationByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await _fixture.GetHttpResult(UrlPath + InvalidApplicationId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void CreateCommentForApplicationShouldCreateAnewComment()
        {
            var applicationToTest = await _fixture.GetHttpResult(UrlPath + _applicationId);
            var currentNumberOfComments =
                JsonConvert.DeserializeObject<ApplicationDetailDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                    .Comments.Count();

            var newComment = new CommentCreateDto()
            {
                IsPrivate = false,
                RequiresChanges = true,
                Message = "Test Kommentar",
                // TODO Look at that again!!!
                UserId =
                    JsonConvert.DeserializeObject<ApplicationDetailDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                        .User.Id
            };

            var serializedComment = JsonConvert.SerializeObject(newComment);
            var result = await _fixture.PostHttpResult(UrlPath + _applicationId + "/comments/", serializedComment);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            applicationToTest = await _fixture.GetHttpResult(UrlPath + _applicationId);
            var newNumberOfComments = JsonConvert.DeserializeObject<ApplicationDetailDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                    .Comments.Count();

            newNumberOfComments.Should().Be(currentNumberOfComments + 1);
        }

        [Fact]
        public async void CreateCommentWithInvalidModelStateShouldReturnBadRequestAndCreateNoNewEntry()
        {
            var applicationToTest = await _fixture.GetHttpResult(UrlPath + _applicationId);
            var currentNumberOfComments =
                JsonConvert.DeserializeObject<ApplicationDetailDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                    .Comments.Count();

            var newComment = new CommentCreateDto()
            {
                IsPrivate = false,
                RequiresChanges = true,
                Message = "",
                // TODO Look at that again!!!
                UserId =
                    JsonConvert.DeserializeObject<ApplicationDetailDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                        .User.Id
            };

            var serializedComment = JsonConvert.SerializeObject(newComment);
            var result = await _fixture.PostHttpResult(UrlPath + _applicationId + "/comments/", serializedComment);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            applicationToTest = await _fixture.GetHttpResult(UrlPath + _applicationId);

            var newNumberOfComments =
                JsonConvert.DeserializeObject<ApplicationDetailDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                    .Comments.Count();
            newNumberOfComments.Should().Be(currentNumberOfComments);
        }

        [Fact]
        public async void CreateApplicationShouldReturnCreated()
        {

            var newApplication = new ApplicationCreateDto()
            {
                FormId = DataSeeder.FormId1,
                StatusId = StatusValue.CREATED,
                UserId = DataSeeder.UserId1,
                FilledForm = "{\"1\":\"Messi\",\"2\":\"Rolando\"}"
            };

            var serializedApplication = JsonConvert.SerializeObject(newApplication);
            var result = await _fixture.PostHttpResult(UrlPath, serializedApplication);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async void CreateApplicationWithInvalidModelStateShouldReturnBadRequest()
        {

            var newApplication = new ApplicationCreateDto()
            {
                FormId = DataSeeder.FormId1,
                StatusId = StatusValue.CREATED,
                FilledForm = "{\"1\":\"Messi\",\"2\":\"Rolando\"}"
            };

            var serializedApplication = JsonConvert.SerializeObject(newApplication);
            var result = await _fixture.PostHttpResult(UrlPath, serializedApplication);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void UpdateApplicationShouldCreateNewApplicationWithUpdatedVersion()
        {
            var result = await _fixture.GetHttpResult(UrlPath + _applicationToUpdateId);
            var applicationToUpdate = JsonConvert.DeserializeObject<ApplicationDetailDto>(result.Content.ReadAsStringAsync().Result);

            var newCreateDto = new ApplicationCreateDto()
            {
                ConferenceId = applicationToUpdate.Conference?.Id,
                FilledForm = "{\"1\":\"Messi\",\"2\":\"Rolando\"}",
                FormId = applicationToUpdate.Form.Id,
                StatusId = applicationToUpdate.Status,
                UserId = applicationToUpdate.User.Id
            };

            var serializedApplication = JsonConvert.SerializeObject(newCreateDto);
            var response = await _fixture.PutHttpResult($"{UrlPath}{_applicationToUpdateId}", serializedApplication);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var deserializedResponse = JsonConvert.DeserializeObject<ApplicationDetailDto>(response.Content.ReadAsStringAsync().Result);
            deserializedResponse.Version.Should().Be(applicationToUpdate.Version + 1);

            deserializedResponse.PreviousVersion.Should().Be(applicationToUpdate.Id);

            deserializedResponse.Comments.Count().Should().Be(applicationToUpdate.Comments.Count());

            deserializedResponse.Assignments.Count().Should().Be(applicationToUpdate.Assignments.Count());
            deserializedResponse.Assignments.Select(userDto => userDto.Id).ToList().ForEach(guid =>
            {
                applicationToUpdate.Assignments.Select(userDto => userDto.Id).Contains(guid).Should().Be(true);
            });
        }


        [Fact]
        public async void DeleteApplicationByIdShouldReturnOkAndDeleteOneApplication()
        {
            var httpResponse = await _fixture.GetHttpResult(UrlPath + _applicationToDelete);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await _fixture.DeleteHttpResult(UrlPath + _applicationToDelete);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var httpResponseOfDeletedConference = await _fixture.GetHttpResult(UrlPath + _applicationToDelete);
            httpResponseOfDeletedConference.Should().NotBeNull();
            httpResponseOfDeletedConference.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void DeleteApplicationWithInvalidIdShouldReturnNotFound()
        {
            var httpResponse = await _fixture.GetHttpResult(UrlPath + InvalidApplicationId);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void UnassignUserFromApplicationShouldReturnOkAndUnassignTheUser()
        {
            var httpResponse = await _fixture.DeleteHttpResult($"{UrlPath}{_applicationId}/assignments/{_userId1}");
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void UnassignUserWithInvalidFromApplicationShouldReturnNotFound()
        {
            var httpResponse = await _fixture.DeleteHttpResult($"{UrlPath}{_applicationId}/assignments/{_invalidUserIdToUnassign}");
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void AssignUserToApplicationShouldReturnOkAndAssignThisUserToTheApplication()
        {
            var assignmentCreateDto = new AssignmentCreateDto()
            {
                UserId = _userId3
            };

            var serializedAssignmentCreateDto = JsonConvert.SerializeObject(assignmentCreateDto);
            var httpResponse = await _fixture.PostHttpResult($"{UrlPath}{_applicationId4}/assignments/", serializedAssignmentCreateDto);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async void AssignAllreadyAssignedUserToApplicationShouldReturnBadRequest()
        {
            var assignmentCreateDto = new AssignmentCreateDto()
            {
                UserId = _userId1
            };

            var serializedAssignmentCreateDto = JsonConvert.SerializeObject(assignmentCreateDto);
            var httpResponse = await _fixture.PostHttpResult($"{UrlPath}{_applicationId4}/assignments/", serializedAssignmentCreateDto);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}