using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    [Collection("Database collection")]
    public class ApplicationEndpointTest
    {
        private readonly DatabaseFixture _fixture;
        private const string UrlPath = "/applications/";
        private readonly Guid _applicationId = DataSeeder.ApplicationId1;
        private readonly Guid _applicationToUpdateId = DataSeeder.ApplicationId2;
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
                JsonConvert.DeserializeObject<List<ApplicationDto>>(result.Content.ReadAsStringAsync().Result);
            applications.ForEach(dto => dto.Should().BeOfType<ApplicationDto>());
        }

        [Fact]
        public async void GetApplicationByIdShouldReturnOneElement()
        {
            var result = await _fixture.GetHttpResult(UrlPath + _applicationId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var application = JsonConvert.DeserializeObject<ApplicationDto>(result.Content.ReadAsStringAsync().Result);
            application.Should().BeOfType<ApplicationDto>();
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
                JsonConvert.DeserializeObject<ApplicationDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                    .Comments.Count();

            var newComment = new CommentCreateDto()
            {
                IsPrivate = false,
                RequiresChanges = true,
                Text = "Test Kommentar",
                UserId =
                    JsonConvert.DeserializeObject<ApplicationDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                        .UserId
            };

            var serializedComment = JsonConvert.SerializeObject(newComment);
            var result = await _fixture.PostHttpResult(UrlPath + _applicationId + "/comments/", serializedComment);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            applicationToTest = await _fixture.GetHttpResult(UrlPath + _applicationId);
            var newNumberOfComments = JsonConvert.DeserializeObject<ApplicationDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                    .Comments.Count();

            newNumberOfComments.Should().Be(currentNumberOfComments + 1);
        }

        [Fact]
        public async void CreateCommentWithInvalidModelStateShouldReturnBadRequestAndCreateNoNewEntry()
        {
            var applicationToTest = await _fixture.GetHttpResult(UrlPath + _applicationId);
            var currentNumberOfComments =
                JsonConvert.DeserializeObject<ApplicationDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                    .Comments.Count();

            var newComment = new CommentCreateDto()
            {
                IsPrivate = false,
                RequiresChanges = true,
                Text = "",
                UserId =
                    JsonConvert.DeserializeObject<ApplicationDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                        .UserId
            };

            var serializedComment = JsonConvert.SerializeObject(newComment);
            var result = await _fixture.PostHttpResult(UrlPath + _applicationId + "/comments/", serializedComment);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            applicationToTest = await _fixture.GetHttpResult(UrlPath + _applicationId);

            var newNumberOfComments =
                JsonConvert.DeserializeObject<ApplicationDto>(applicationToTest.Content.ReadAsStringAsync().Result)
                    .Comments.Count();
            newNumberOfComments.Should().Be(currentNumberOfComments);
        }

        [Fact]
        public async void CreateApplicationShouldReturnCreated()
        {

            var assignments = new List<Guid>();
            assignments.Add(DataSeeder.UserId2);
            assignments.Add(DataSeeder.UserId1);

            var newApplication = new ApplicationCreateDto()
            {
                FormId = DataSeeder.FormId1,
                IsCurrent = true,
                StatusId = DataSeeder.StatusId1,
                UserId = DataSeeder.UserId1,
                Version = 0,
                FilledForm = "{\"1\":\"Messi\",\"2\":\"Rolando\"}",
                Assignments = assignments
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
                IsCurrent = true,
                StatusId = DataSeeder.StatusId1,
                Version = 0,
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
            var applicationToUpdate = JsonConvert.DeserializeObject<ApplicationDto>(result.Content.ReadAsStringAsync().Result);

            var newCreateDto = new ApplicationCreateDto()
            {
                ConferenceId = applicationToUpdate.ConferenceId ?? null,
                FilledForm = "{\"1\":\"Messi\",\"2\":\"Rolando\"}",
                FormId = applicationToUpdate.FormId,
                StatusId = applicationToUpdate.StatusId,
                IsCurrent = applicationToUpdate.IsCurrent,
                PreviousVersion = applicationToUpdate.Id,
                UserId = applicationToUpdate.UserId,
                Version = applicationToUpdate.Version + 1,
                Assignments = applicationToUpdate.Assignments.ToList(),
                Comments = applicationToUpdate.Comments.Select(dto => dto.Id).ToList()
            };

            var serializedApplication = JsonConvert.SerializeObject(newCreateDto);
            var response = await _fixture.PutHttpResult($"{UrlPath}{_applicationToUpdateId}", serializedApplication);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var deserializedResponse = JsonConvert.DeserializeObject<ApplicationDto>(response.Content.ReadAsStringAsync().Result);
            deserializedResponse.Version.Should().Be(applicationToUpdate.Version + 1);

            deserializedResponse.PreviousVersion.Should().Be(applicationToUpdate.Id);

            deserializedResponse.Comments.Count().Should().Be(applicationToUpdate.Comments.Count());

            deserializedResponse.Assignments.Count().Should().Be(applicationToUpdate.Assignments.Count());
            deserializedResponse.Assignments.ToList().ForEach(guid =>
            {
                applicationToUpdate.Assignments.Contains(guid).Should().Be(true);
            });
        }
    }
}