using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test.Integration
{
    [Collection("Test collection")]
    public class UserEndpointTest
    {
        private readonly DatabaseFixture _fixture;
        private const string UrlPath = "/users/";
        private readonly Guid _userId1 = DataSeeder.UserId1;
        private readonly Guid _userId2 = DataSeeder.UserId2;
        private readonly Guid _userId3 = DataSeeder.UserId3;
        private readonly Guid _roleId1 = DataSeeder.RoleId1;
        private const int InvalidUserId = 98765;
        private const int InvalidRoleId = 123456;

        public UserEndpointTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetAllUsersShouldReturnStatusCodeOk()
        {
            var result = await _fixture.GetHttpResult(UrlPath);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var users = JsonConvert.DeserializeObject<List<UserDetailDto>>(result.Content.ReadAsStringAsync().Result);
            users.ForEach(dto => dto.Should().BeOfType<UserDetailDto>());
        }

        [Fact]
        public async void GetUserByIdShouldReturnOneUser()
        {
            var result = await _fixture.GetHttpResult(UrlPath + _userId1);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var user = JsonConvert.DeserializeObject<UserDetailDto>(result.Content.ReadAsStringAsync().Result);
            user.Should().BeOfType<UserDetailDto>();
        }

        [Fact]
        public async void GetUserByIdShouldReturnNotFoundForInvalidId()
        {
            var result = await _fixture.GetHttpResult(UrlPath + InvalidUserId);
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async void AddRoleToUserShouldReturnOk()
        {
            var roleDto = new RoleDto()
            {
                Id = _roleId1,
                Name = "Admin"
            };

            var serializedRoleDto = JsonConvert.SerializeObject(roleDto);
            var httpResponse = await _fixture.PostHttpResult($"{UrlPath}{_userId3}/role", serializedRoleDto);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void AddAllreadyAssignedRoleToUserShouldReturnBadRequest()
        {
            var roleDto = new RoleDto()
            {
                Id = _roleId1,
                Name = "Admin"
            };

            var serializedRoleDto = JsonConvert.SerializeObject(roleDto);
            var httpResponse = await _fixture.PostHttpResult($"{UrlPath}{_userId1}/role", serializedRoleDto);
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void RemoveRoleFromUserShouldReturnOk()
        {
            var httpResponse = await _fixture.DeleteHttpResult($"{UrlPath}{_userId2}/role/{_roleId1}");
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void RemoveRoleWithInvalidIdFromUserShouldReturnNotFound()
        {
            var httpResponse = await _fixture.DeleteHttpResult($"{UrlPath}{_userId2}/role/{InvalidRoleId}");
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


    }
}