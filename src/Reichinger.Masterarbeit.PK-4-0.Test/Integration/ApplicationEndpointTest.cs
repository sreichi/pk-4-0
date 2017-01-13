using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Novell.Directory.Ldap.Utilclass;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    [Collection("Database collection")]
    public class ApplicationEndpointTest
    {
        private readonly DatabaseFixture _fixture;
        private const string UrlPath = "/applications/";
        private readonly Guid _applicationId = new Guid("86c42368-ba33-4fca-a911-fa8d3758b01d");
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
            applications.Count.Should().Be(2);
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
            assignments.Add(new Guid("ee632373-432e-40f0-9f33-8cc6b684e673"));
            assignments.Add(new Guid("b904cc6e-b3a6-42a9-8880-3096be1b6c61"));

            var newApplication = new ApplicationCreateDto()
            {
                FormId = new Guid("bb2cf80b-6f7f-4305-8d65-4468908fd1f3"),
                IsCurrent = true,
                StatusId = new Guid("e3c1f89f-d9d5-4d76-a05a-2b3745d72c80"),
                UserId = new Guid("b904cc6e-b3a6-42a9-8880-3096be1b6c61"),
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
                FormId = new Guid("bb2cf80b-6f7f-4305-8d65-4468908fd1f3"),
                IsCurrent = true,
                StatusId = new Guid("e3c1f89f-d9d5-4d76-a05a-2b3745d72c80"),
                Version = 0,
                FilledForm = "{\"1\":\"Messi\",\"2\":\"Rolando\"}"
            };

            var serializedApplication = JsonConvert.SerializeObject(newApplication);
            var result = await _fixture.PostHttpResult(UrlPath, serializedApplication);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}