using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    [Collection("Database collection")]
    public class FormEndpointTest
    {
        private DatabaseFixture _fixture;
        private const string UrlPath = "/forms/";
        private readonly Guid FormId = new Guid("bb2cf80b-6f7f-4305-8d65-4468908fd1f3");
        private const int InvalidFormId = 98765;

        public FormEndpointTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetAllFormsShouldReturnAListofFormDtos()
        {
            var result = await _fixture.GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var forms = JsonConvert.DeserializeObject<List<FormDto>>(result.Content.ReadAsStringAsync().Result);
            forms.ForEach(form => form.Should().BeOfType<FormDto>());
            forms.Count.Should().Be(2);
        }

        [Fact]
        public async void GetFormByIdShouldReturnOneFormDto()
        {
            var result = await _fixture.GetHttpResult(UrlPath + FormId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var form = JsonConvert.DeserializeObject<FormDto>(result.Content.ReadAsStringAsync().Result);
            form.Should().BeOfType<FormDto>();
        }

        [Fact]
        public async void GetFormByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await _fixture.GetHttpResult(UrlPath + InvalidFormId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}