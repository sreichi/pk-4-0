using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
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

        private IEnumerable<FieldCreateDto> fields = new List<FieldCreateDto>();
        private ICollection<Guid> styles = new List<Guid>();
        private ICollection<Guid> validations = new List<Guid>();

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

            var forms = JsonConvert.DeserializeObject<List<FormsDto>>(result.Content.ReadAsStringAsync().Result);
            forms.ForEach(form => form.Should().BeOfType<FormsDto>());
        }

        [Fact]
        public async void GetFormByIdShouldReturnSingleFormDto()
        {
            var result = await _fixture.GetHttpResult(UrlPath + FormId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var form = JsonConvert.DeserializeObject<SingleFormDto>(result.Content.ReadAsStringAsync().Result);
            form.Should().BeOfType<SingleFormDto>();
        }

        [Fact]
        public async void GetFormByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await _fixture.GetHttpResult(UrlPath + InvalidFormId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async void CreateFormShouldCreateNewEntityAndReturnCreated()
        {

            var allForms = await _fixture.GetHttpResult(UrlPath);
            var currentNumberOfForms =
                JsonConvert.DeserializeObject<List<FormsDto>>(allForms.Content.ReadAsStringAsync().Result).Count;

            styles.Add(new Guid("2674979f-3f39-40bf-a301-6a548f7bde15"));
            validations.Add(new Guid("640dae4d-8cfe-4aec-a98c-9ec23dc842d6"));

            fields.Append(new FieldCreateDto()
            {
                Name = "TestField",
                ContentType = "string",
                Label = "label",
                Placeholder = "placeholder",
                Value = "value",
                Required = true,
                Options = "{\"id\":42 , \"name\":\"Rolando\"}",
                FieldType = new Guid("5c3914e9-a1ea-4c21-914a-39c2b5faa90c"),
                FieldHasStyle = styles,
                FieldHasValidation = validations

            });

            fields.Append(new FieldCreateDto()
            {
                Name = "FieldTest",
                ContentType = "integer",
                Label = "bezeichnung",
                Placeholder = "platz",
                Value = "Wert",
                Required = false,
                Options = "{\"id\":69 , \"name\":\"Messi\"}",
                FieldType = new Guid("5c3914e9-a1ea-4c21-914a-39c2b5faa90c"),
                FieldHasValidation = validations
            });

            var newForm = new FormCreateDto()
            {
                Name = "NewTest Form",
                IsPublic = true,
                RestrictedAccess = false,
                FormHasField = fields
            };

            var serializedForm = JsonConvert.SerializeObject(newForm);

            var result = await _fixture.PostHttpResult(UrlPath, serializedForm);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.Created);


            allForms = await _fixture.GetHttpResult(UrlPath);
            var newNumberOfForms = JsonConvert.DeserializeObject<List<FormsDto>>(allForms.Content.ReadAsStringAsync().Result).Count;

            newNumberOfForms.Should().Be(currentNumberOfForms + 1);
        }

    }
}