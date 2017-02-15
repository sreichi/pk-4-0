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
    public class CommentEndpointTest
    {

        private readonly DatabaseFixture _fixture;
        private const string UrlPath = "/comments/";
        private readonly Guid _commentId = DataSeeder.CommentId1;

        public CommentEndpointTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetAllConferencesShouldReturnAListOfConferenceDtos()
        {
            var result = await _fixture.GetHttpResult(UrlPath + _commentId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var conferences = JsonConvert.DeserializeObject<CommentDto>(result.Content.ReadAsStringAsync().Result);
            conferences.Should().BeOfType(typeof(CommentDto));
        }

    }
}