﻿using System;
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
    [Collection("Test collection")]
    public class FormEndpointTest
    {
        private readonly DatabaseFixture _fixture;
        private const string UrlPath = "/forms/";
        private readonly Guid _formId = DataSeeder.FormId1;
        private readonly Guid _inactiveForm = DataSeeder.FormId3;
        private readonly Guid _formToDeleteId = DataSeeder.FormId2;
        private const int InvalidFormId = 98765;

        private readonly IEnumerable<FieldCreateDto> _fields = new List<FieldCreateDto>();
        private readonly ICollection<Guid> _styles = new List<Guid>();
        private readonly ICollection<Guid> _validations = new List<Guid>();

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

            var forms = JsonConvert.DeserializeObject<List<FormListDto>>(result.Content.ReadAsStringAsync().Result);
            forms.ForEach(form => form.Should().BeOfType<FormListDto>());
        }

        [Fact]
        public async void GetFormByIdShouldReturnSingleFormDto()
        {
            var result = await _fixture.GetHttpResult(UrlPath + _formId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var form = JsonConvert.DeserializeObject<FormDetailDto>(result.Content.ReadAsStringAsync().Result);
            form.Should().BeOfType<FormDetailDto>();
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
                JsonConvert.DeserializeObject<List<FormListDto>>(allForms.Content.ReadAsStringAsync().Result).Count;

            _styles.Add(DataSeeder.StyleId1);
            _validations.Add(DataSeeder.ValidationId1);

            _fields.Append(new FieldCreateDto()
            {
                Name = "TestField",
                ContentType = "string",
                Label = "label",
                Placeholder = "placeholder",
                Value = "value",
                Required = true,
                OptionsJson = "{\"id\":42 , \"name\":\"Rolando\"}",
                FieldType = DataSeeder.FieldTypeId1,
                StyleIds = _styles,
                ValidationIds = _validations

            });

            _fields.Append(new FieldCreateDto()
            {
                Name = "FieldTest",
                ContentType = "integer",
                Label = "bezeichnung",
                Placeholder = "platz",
                Value = "Wert",
                Required = false,
                OptionsJson = "{\"id\":69 , \"name\":\"Messi\"}",
                FieldType = DataSeeder.FieldTypeId1,
                ValidationIds = _validations
            });

            var newForm = new FormCreateDto()
            {
                Title = "NewTest Form",
                IsPublic = true,
                RestrictedAccess = false,
                FormHasField = _fields
            };

            var serializedForm = JsonConvert.SerializeObject(newForm);

            var result = await _fixture.PostHttpResult(UrlPath, serializedForm);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.Created);


            allForms = await _fixture.GetHttpResult(UrlPath);
            var newNumberOfForms = JsonConvert.DeserializeObject<List<FormListDto>>(allForms.Content.ReadAsStringAsync().Result).Count;

            newNumberOfForms.Should().Be(currentNumberOfForms + 1);
        }

        [Fact]
        public async void CreateNewFormWithInvalidModelStateShouldReturnBadRequest()
        {
            _fields.Append(new FieldCreateDto()
            {
                Name = "TestField",
                OptionsJson = "{\"id\":42 , \"name\":\"Rolando\"}",
                FieldType = DataSeeder.FieldTypeId1,
            });

            var newForm = new FormCreateDto()
            {
                IsPublic = true,
                RestrictedAccess = false,
                FormHasField = _fields
            };

            var serializedForm = JsonConvert.SerializeObject(newForm);
            var result = await _fixture.PostHttpResult(UrlPath, serializedForm);

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void DeleteFormShouldReturnOkAndDeleteOneEntity()
        {
            var result = await _fixture.DeleteHttpResult(UrlPath + _formToDeleteId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var tryToLoadDeletedForm = await _fixture.GetHttpResult(UrlPath + _formToDeleteId);
            tryToLoadDeletedForm.Should().NotBeNull();
            tryToLoadDeletedForm.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void DeleteFormWithApplicationsShouldReturnBadRequest()
        {
            var result = await _fixture.DeleteHttpResult(UrlPath + _formId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void ActivateFormShouldReturnOkAndSetFormToActive()
        {
            var result = await _fixture.PutHttpResult($"{UrlPath}{_inactiveForm}/activate", "");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var activatedForm = await _fixture.GetHttpResult(UrlPath + _inactiveForm);
            var deserializedForm = JsonConvert.DeserializeObject<FormDetailDto>(activatedForm.Content.ReadAsStringAsync().Result);
            deserializedForm.IsActive.Should().Be(true);
        }
    }
}