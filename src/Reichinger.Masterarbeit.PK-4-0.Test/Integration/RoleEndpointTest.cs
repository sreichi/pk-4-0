﻿using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    [Collection("Database collection")]
    public class RoleEndpointTest
    {
        private readonly DatabaseFixture _fixture;
        private const string UrlPath = "/roles/";
        private readonly Guid _roleId = DataSeeder.RoleId1;
        private const int InvalidRoleId = 987654;

        public RoleEndpointTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetAllRolesShouldReturnAListOfRoleDtos()
        {
            var result = await _fixture.GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var applications = JsonConvert.DeserializeObject<List<RoleDto>>(result.Content.ReadAsStringAsync().Result);
            applications.Count.Should().Be(2);
            applications.ForEach(dto => dto.Should().BeOfType<RoleDto>());
        }

        [Fact]
        public async void GetRoleByIdShouldReturnOneElement()
        {
            var result = await _fixture.GetHttpResult(UrlPath + _roleId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var application = JsonConvert.DeserializeObject<RoleDto>(result.Content.ReadAsStringAsync().Result);
            application.Should().BeOfType<RoleDto>();
        }

        [Fact]
        public async void GetRoleByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await _fixture.GetHttpResult(UrlPath + InvalidRoleId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}