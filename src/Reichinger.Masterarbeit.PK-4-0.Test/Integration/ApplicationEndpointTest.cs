using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
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
                ApplicationId = _applicationId,
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
                ApplicationId = _applicationId,
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
    }
}