﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database
{
    // this class seeds the Application with test data.
    // the test data is important for the tests of the application
    public static class DataSeeder
    {
        public static readonly Guid RoleId1 = new Guid("8fa4497d-bee4-411d-83ef-f195037cbb43");
        public static readonly Guid RoleId2 = new Guid("c18ac352-9efc-4d64-8555-3379336309c8");
        public static readonly Guid RoleId3 = new Guid("0FAA2DAD-53E8-4EE5-9CD8-CB1B9E5578BC");

        public static readonly Guid UserId1 = new Guid("b904cc6e-b3a6-42a9-8880-3096be1b6c61");
        public static readonly Guid UserId2 = new Guid("ee632373-432e-40f0-9f33-8cc6b684e673");
        public static readonly Guid UserId3 = new Guid("ec6f8f65-b076-4cfe-9c2d-e486448b083b");

        public static readonly Guid ApplicationId1 = new Guid("86c42368-ba33-4fca-a911-fa8d3758b01d");
        public static readonly Guid ApplicationId2 = new Guid("b630df84-32bd-42b5-a957-7f94a9ee503e");
        public static readonly Guid ApplicationId3 = new Guid("adf4f223-01a0-4f57-971b-46785a8bd80c");
        public static readonly Guid ApplicationId4 = new Guid("2e0fc5b8-9f12-4ded-86aa-00fb6eeccc16");

        public static readonly Guid FormId1 = new Guid("bb2cf80b-6f7f-4305-8d65-4468908fd1f3");
        public static readonly Guid FormId2 = new Guid("e5253303-5f6e-474e-812b-d655afce5edb");
        public static readonly Guid FormId3 = new Guid("D9CB5464-5271-4F6E-9D32-DCCF9DABFE08");

        public static readonly Guid ConferenceId1 = new Guid("74cf7b5c-1c7e-448b-ac5d-b9c63d466e1a");
        public static readonly Guid ConferenceId2 = new Guid("866ad5a9-64a4-4058-9751-b0dd27ef4d0e");
        public static readonly Guid ConferenceId3 = new Guid("18d1cf10-420e-4230-9cf8-b9bde3155b8b");

        public static readonly Guid StatusId1 = new Guid("e3c1f89f-d9d5-4d76-a05a-2b3745d72c80");
        public static readonly Guid StatusId2 = new Guid("ba72b0fb-9969-4942-801e-685b86059421");

        public static readonly Guid FieldId1 = new Guid("3eeecb10-c634-444d-8619-e924df38cd3c");
        public static readonly Guid FieldId2 = new Guid("7ac9cb82-f717-46e6-85e1-03e8783312a8");
        public static readonly Guid FieldId3 = new Guid("1cf6b5ef-a891-4d56-8212-e04450616543");
        public static readonly Guid FieldId4 = new Guid("5c46387f-613d-4f0d-bfec-52e9f7051cb0");
        public static readonly Guid FieldId5 = new Guid("e1a7e22b-c482-41a5-806b-f9bc9d20891b");

        public static readonly Guid FieldTypeId1 = new Guid("5c3914e9-a1ea-4c21-914a-39c2b5faa90c");
        public static readonly Guid FieldTypeId2 = new Guid("77c630ab-8298-4847-a1f0-7cccc4172ea4");
        public static readonly Guid FieldTypeId3 = new Guid("8abb2070-3abb-47b7-b50d-54b0d9b3db5d");
        public static readonly Guid FieldTypeId4 = new Guid("3bd87eea-6b12-45c1-b3ec-0cb89f2f7115");

        public static readonly Guid CommentId1 = new Guid("df8452a4-de1d-4013-888c-4b486da7f269");
        public static readonly Guid CommentId2 = new Guid("4f8cd9b9-eb6a-4f56-b481-f35a1fd83493");
        public static readonly Guid CommentId3 = new Guid("ec243020-2158-4c26-87be-f3a83042fec1");

        public static readonly Guid StyleId1 = new Guid("2674979f-3f39-40bf-a301-6a548f7bde15");
        public static readonly Guid StyleId2 = new Guid("78540ab7-51e0-4d67-82bc-8efb1d679d03");

        public static readonly Guid ValidationId1 = new Guid("640dae4d-8cfe-4aec-a98c-9ec23dc842d6");
        public static readonly Guid ValidationId2 = new Guid("55f37c8b-56aa-4b4f-8f14-b500854e11a9");

        public static readonly Guid ConfigId1 = new Guid("8a2222b0-e603-44b9-aa20-f4b0086db429");
        public static readonly Guid ConfigId2 = new Guid("42dcc7fc-c462-4684-8a6b-c55860708dea");

        public static readonly Guid PermissionId1 = new Guid("36DFAC33-AE79-48C2-AB06-21D3D2C0899B");
        public static readonly Guid PermissionId2 = new Guid("06A7565E-633E-428C-A269-E225332DE6FB");
        public static readonly Guid PermissionId3 = new Guid("354BFC04-9563-4939-9142-1F89C2BB02A8");

        public static void SeedData(this IApplicationBuilder applicationBuilder)
        {
            var db =
                applicationBuilder.ApplicationServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            CreateTestData(db);
        }

        private static void CreateTestData(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();

            CreateRoles(dbContext);
            CreateUsers(dbContext);
            CreateForms(dbContext);
            CreateConferences(dbContext);
            CreateApplications(dbContext);
            CreateAssignments(dbContext);
            CreateAttendants(dbContext);
            CreateComments(dbContext);
            CreateFieldTypes(dbContext);
            CreateFields(dbContext);
            CreateStyles(dbContext);
            CreateValidations(dbContext);
            CreateFormHasField(dbContext);
            CreateFieldHasStyle(dbContext);
            CreateConfigs(dbContext);
            CreateFieldTypeHasStyle(dbContext);
            CreateFieldTypeHasValidation(dbContext);
            CreateFieldTypeHasConfig(dbContext);
            CreaFieldHasValidation(dbContext);
            CreateUserHasRole(dbContext);
            CreatePermissions(dbContext);
            CreateRolePermissions(dbContext);
            dbContext.SaveChanges();
        }

        private static void CreateRolePermissions(ApplicationDbContext dbContext)
        {
            dbContext.RolePermission.Add(new RolePermission()
            {
                RoleId = RoleId1,
                PermissionId = PermissionId1
            });
            dbContext.RolePermission.Add(new RolePermission()
            {
                RoleId = RoleId1,
                PermissionId = PermissionId2
            });
        }

        private static void CreatePermissions(ApplicationDbContext dbContext)
        {
            dbContext.Add(new Permission()
            {
                Id = PermissionId1,
                Name = "ALL.EDIT"
            });
            dbContext.Add(new Permission()
            {
                Id = PermissionId2,
                Name = "ALL.VIEW"
            });
            dbContext.Add(new Permission()
            {
                Id = PermissionId3,
                Name = "ALL.DELETE"
            });
        }

        private static void CreateAttendants(ApplicationDbContext dbContext)
        {
            dbContext.Attendant.Add(new Attendant()
            {
                ConferenceId = ConferenceId1,
                UserId = UserId2,
                TypeOfAttendance = TypeOfAttendance.GUEST
            });
            dbContext.Attendant.Add(new Attendant()
            {
                ConferenceId = ConferenceId1,
                UserId = UserId1,
                TypeOfAttendance = TypeOfAttendance.MEMBER
            });
            dbContext.Attendant.Add(new Attendant()
            {
                ConferenceId = ConferenceId2,
                UserId = UserId1,
                TypeOfAttendance = TypeOfAttendance.MEMBER
            });
        }

        private static void CreateFieldTypeHasConfig(ApplicationDbContext dbContext)
        {
            dbContext.TypeHasConfig.Add(new TypeHasConfig()
            {
                FieldTypeId = FieldTypeId1,
                Position = 1,
                ConfigId = ConfigId1
            });
            dbContext.TypeHasConfig.Add(new TypeHasConfig()
            {
                FieldTypeId = FieldTypeId1,
                Position = 2,
                ConfigId = ConfigId2
            });
            dbContext.TypeHasConfig.Add(new TypeHasConfig()
            {
                FieldTypeId = FieldTypeId2,
                Position = 1,
                ConfigId = ConfigId1
            });
            dbContext.TypeHasConfig.Add(new TypeHasConfig()
            {
                FieldTypeId = FieldTypeId3,
                Position = 1,
                ConfigId = ConfigId2
            });
            dbContext.TypeHasConfig.Add(new TypeHasConfig()
            {
                FieldTypeId = FieldTypeId4,
                Position = 1,
                ConfigId = ConfigId1
            });
            dbContext.TypeHasConfig.Add(new TypeHasConfig()
            {
                FieldTypeId = FieldTypeId4,
                Position = 2,
                ConfigId = ConfigId2
            });
        }

        private static void CreateFieldTypeHasValidation(ApplicationDbContext dbContext)
        {
            dbContext.TypeHasValidation.Add(new TypeHasValidation()
            {
                FieldTypeId = FieldTypeId1,
                ValidationId = ValidationId1,
            });
            dbContext.TypeHasValidation.Add(new TypeHasValidation()
            {
                FieldTypeId = FieldTypeId1,
                ValidationId = ValidationId2,
            });
            dbContext.TypeHasValidation.Add(new TypeHasValidation()
            {
                FieldTypeId = FieldTypeId2,
                ValidationId = ValidationId1,
            });
            dbContext.TypeHasValidation.Add(new TypeHasValidation()
            {
                FieldTypeId = FieldTypeId3,
                ValidationId = ValidationId2,
            });
            dbContext.TypeHasValidation.Add(new TypeHasValidation()
            {
                FieldTypeId = FieldTypeId4,
                ValidationId = ValidationId1,
            });
            dbContext.TypeHasValidation.Add(new TypeHasValidation()
            {
                FieldTypeId = FieldTypeId4,
                ValidationId = ValidationId2,
            });
        }

        private static void CreateFieldTypeHasStyle(ApplicationDbContext dbContext)
        {
            dbContext.TypeHasStyle.Add(new TypeHasStyle()
            {
                FieldTypeId = FieldTypeId1,
                StyleId = StyleId1
            });
            dbContext.TypeHasStyle.Add(new TypeHasStyle()
            {
                FieldTypeId = FieldTypeId1,
                StyleId = StyleId2
            });
            dbContext.TypeHasStyle.Add(new TypeHasStyle()
            {
                FieldTypeId = FieldTypeId2,
                StyleId = StyleId1
            });
            dbContext.TypeHasStyle.Add(new TypeHasStyle()
            {
                FieldTypeId = FieldTypeId3,
                StyleId = StyleId2
            });
            dbContext.TypeHasStyle.Add(new TypeHasStyle()
            {
                FieldTypeId = FieldTypeId4,
                StyleId = StyleId1
            });
            dbContext.TypeHasStyle.Add(new TypeHasStyle()
            {
                FieldTypeId = FieldTypeId4,
                StyleId = StyleId2
            });
        }

        private static void CreateConfigs(ApplicationDbContext dbContext)
        {
            dbContext.Config.Add(new Config()
            {
                Id = ConfigId1,
                Value = "{\"1\":\"McDoof\",\"2\":\"Würger King\"}",
                Name = "Config1"
            });
            dbContext.Config.Add(new Config()
            {
                Id = ConfigId2,
                Value = "{\"1\":\"Messi\",\"2\":\"Rolando\"}",
                Name = "Config2"
            });
        }

        private static void CreaFieldHasValidation(ApplicationDbContext dbContext)
        {
            dbContext.FieldHasValidation.Add(new FieldHasValidation()
            {
                FieldId = FieldId1,
                ValidationId = ValidationId2
            });

            dbContext.FieldHasValidation.Add(new FieldHasValidation()
            {
                FieldId = FieldId1,
                ValidationId = ValidationId1
            });

            dbContext.FieldHasValidation.Add(new FieldHasValidation()
            {
                FieldId = FieldId2,
                ValidationId = ValidationId2
            });
        }

        private static void CreateFieldHasStyle(ApplicationDbContext dbContext)
        {
            dbContext.FieldHasStyle.Add(new FieldHasStyle()
            {
                FieldId = FieldId1,
                StyleId = StyleId1,
            });
            dbContext.FieldHasStyle.Add(new FieldHasStyle()
            {
                FieldId = FieldId2,
                StyleId = StyleId1,
            });
            dbContext.FieldHasStyle.Add(new FieldHasStyle()
            {
                FieldId = FieldId2,
                StyleId = StyleId2,
            });
        }

        private static void CreateValidations(ApplicationDbContext dbContext)
        {
            dbContext.Validation.Add(new Validation()
            {
                Id = ValidationId2,
                ValidationString = "max-lenght-20",
                Description = "Max Length 20 Chars"
            });
            dbContext.Validation.Add(new Validation()
            {
                Id = ValidationId1,
                ValidationString = "min-length-8",
                Description = "Min Length 8 Chars"
            });
        }

        private static void CreateStyles(ApplicationDbContext dbContext)
        {
            dbContext.Style.Add(new Style()
            {
                Id = StyleId1,
                StyleString = "small",
                Description = "Small"
            });
            dbContext.Style.Add(new Style()
            {
                Id = StyleId2,
                StyleString = "alligned",
                Description = "Alligned"
            });
        }


        private static void CreateRoles(ApplicationDbContext dbContext)
        {
            dbContext.Role.Add(
                new Role
                {
                    Id = RoleId1,
                    Name = "Admin"
                });
            dbContext.Role.Add(
                new Role
                {
                    Id = RoleId2,
                    Name = "User"
                });
            dbContext.Role.Add(
                new Role
                {
                    Id = RoleId3,
                    Name = "Student"
                });
        }

        private static void CreateUserHasRole(ApplicationDbContext dbContext)
        {
            dbContext.UserHasRole.Add(new UserHasRole
            {
                RoleId = RoleId1,
                UserId = UserId1
            });
            dbContext.UserHasRole.Add(new UserHasRole
            {
                RoleId = RoleId2,
                UserId = UserId1
            });
            dbContext.UserHasRole.Add(new UserHasRole
            {
                RoleId = RoleId1,
                UserId = UserId2
            });
        }

        private static void CreateFormHasField(ApplicationDbContext dbContext)
        {
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = FormId1,
                FieldId = FieldId1,
                Position = 1
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = FormId1,
                FieldId = FieldId2,
                Position = 2
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = FormId1,
                FieldId = FieldId3,
                Position = 3
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = FormId1,
                FieldId = FieldId4,
                Position = 4
            });
        }

        private static void CreateFields(ApplicationDbContext dbContext)
        {
            dbContext.Field.Add(new Field
            {
                Id = FieldId1,
                FieldType = FieldTypeId1,
                Name = "Firstname"
            });
            dbContext.Field.Add(new Field
            {
                Id = FieldId2,
                FieldType = FieldTypeId1,
                Name = "Lastname"
            });
            dbContext.Field.Add(new Field
            {
                Id = FieldId3,
                FieldType = FieldTypeId3,
                Name = "Gender"
            });
            dbContext.Field.Add(new Field
            {
                Id = FieldId4,
                FieldType = FieldTypeId2,
                Name = "Active"
            });
            dbContext.Field.Add(new Field
            {
                Id = FieldId5,
                FieldType = FieldTypeId4,
                Name = "Beschreibung"
            });
        }

        private static void CreateFieldTypes(ApplicationDbContext dbContext)
        {
            dbContext.FieldType.Add(new FieldType
            {
                Id = FieldTypeId1,
                Label = "INPUT",
                Value = "input"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = FieldTypeId2,
                Label = "CHECKBOX",
                Value = "checkbox"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = FieldTypeId3,
                Label = "RADIO",
                Value = "radio"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = FieldTypeId4,
                Label = "TEXTAREA",
                Value = "textarea"
            });
        }

        private static void CreateComments(ApplicationDbContext dbContext)
        {
            dbContext.Comment.Add(new Comment
            {
                Id = CommentId1,
                ApplicationId = ApplicationId1,
                Created = DateTime.Parse("2016-11-18T13:15:08+00:00"),
                UserId = UserId1,
                Message = "Da fehlt noch was",
                IsPrivate = false
            });
            dbContext.Comment.Add(new Comment
            {
                Id = CommentId2,
                ApplicationId = ApplicationId1,
                Created = DateTime.Parse("2016-10-18T13:15:08+00:00"),
                UserId = UserId2,
                Message = "Ja das Änder ich noch schnell",
                IsPrivate = false
            });
            dbContext.Comment.Add(new Comment
            {
                Id = CommentId3,
                ApplicationId = ApplicationId2,
                Created = DateTime.Parse("2016-11-18T15:15:08+00:00"),
                UserId = UserId1,
                Message = "Jetzt passt alles",
                IsPrivate = false
            });
        }

        private static void CreateConferences(ApplicationDbContext dbContext)
        {
            dbContext.Conference.Add(new Conference
            {
                Id = ConferenceId1,
                DateOfEvent = DateTime.Now,
                Description = "Hauptsitzung der PK",
                StartOfEvent = "08:00",
                EndOfEvent = "11:00",
                RoomOfEvent = "J 2.13",
                NumberOfConference = 112,
                ConferenceConfiguration = "{\"1\":\"McDoof\",\"2\":\"Würger King\"}"
            });
            dbContext.Conference.Add(new Conference
            {
                Id = ConferenceId2,
                DateOfEvent = DateTime.Now,
                Description = "Spontansitzung der Informatik",
                StartOfEvent = "11:00",
                EndOfEvent = "13:00",
                RoomOfEvent = "M 1.01",
                NumberOfConference = 113,
                ConferenceConfiguration = "{\"1\":\"McDoof\",\"2\":\"Würger King\"}"
            });
            dbContext.Conference.Add(new Conference
            {
                Id = ConferenceId3,
                DateOfEvent = DateTime.Now,
                Description = "Abschlusskonferenz 2017",
                StartOfEvent = "17:00",
                EndOfEvent = "19:00",
                RoomOfEvent = "M 1.01",
                NumberOfConference = 114,
                ConferenceConfiguration = "{\"1\":\"McDoof\",\"2\":\"Würger King\"}"
            });
        }

        private static void CreateAssignments(ApplicationDbContext dbContext)
        {
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = ApplicationId1,
                UserId = UserId1
            });
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = ApplicationId1,
                UserId = UserId2
            });
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = ApplicationId2,
                UserId = UserId2
            });
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = ApplicationId4,
                UserId = UserId1
            });
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = ApplicationId4,
                UserId = UserId2
            });
        }



        private static void CreateForms(ApplicationDbContext dbContext)
        {
            dbContext.Form.Add(
                new Form
                {
                    Id = FormId1,
                    Title = "Masterarbeit",
                    Created = DateTime.Now,
                    IsActive = false,
                    Deprecated = false,
                    IsPublic = true,
                    RestrictedAccess = false,
                });
            dbContext.Add(
                new Form
                {
                    Id = FormId2,
                    Title = "Notenanerkennung",
                    Created = DateTime.Now,
                    IsActive = false,
                    IsPublic = false,
                    Deprecated = true,
                    RestrictedAccess = true
                });
            dbContext.Add(
                new Form
                {
                    Id = FormId3,
                    Title = "Inaktive Form",
                    Created = DateTime.Now,
                    IsActive = false,
                    IsPublic = false,
                    Deprecated = true,
                    RestrictedAccess = true
                });
        }

        private static void CreateApplications(ApplicationDbContext dbContext)
        {
            dbContext.Application.Add(
                new Application
                {
                    Id = ApplicationId1,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    Version = 1,
                    IsCurrent = false,
                    PreviousVersion = null,
                    UserId = UserId1,
                    StatusId = StatusValue.DEACTIVATED,
                    FormId = FormId1,
                    ConferenceId = ConferenceId1
                });
            dbContext.Add(
                new Application
                {
                    Id = ApplicationId2,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    Version = 2,
                    IsCurrent = true,
                    PreviousVersion = ApplicationId1,
                    UserId = UserId1,
                    StatusId = StatusValue.CREATED,
                    FormId = FormId1,
                    ConferenceId = ConferenceId1
                });
            dbContext.Add(
                new Application
                {
                    Id = ApplicationId3,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    Version = 0,
                    IsCurrent = true,
                    PreviousVersion = null,
                    UserId = UserId2,
                    StatusId = StatusValue.CREATED,
                    FormId = FormId1,
                    ConferenceId = null
                });
            dbContext.Add(
                new Application
                {
                    Id = ApplicationId4,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    Version = 0,
                    IsCurrent = true,
                    PreviousVersion = null,
                    UserId = UserId2,
                    StatusId = StatusValue.PENDING,
                    FormId = FormId1,
                    ConferenceId = ConferenceId1
                });
        }

        private static void CreateUsers(ApplicationDbContext dbContext)
        {
            var newAppUser = new AppUser
            {
                Id = UserId1,
                Firstname = "Stephan",
                Lastname = "Reichinger",
                Email = "stephan.reichinger@hs-augsburg.de",
                RzName = "reichi",
                LdapId = 15191,
                Active = true,
                Created = DateTime.Now,
                EmployeeType = "Studenten"
            };
            newAppUser.SetHashedPassword("password");
            dbContext.AppUser.Add(newAppUser);

            var newAppUser2 = new AppUser
            {
                Id = UserId2,
                Firstname = "Patrick",
                Lastname = "Schröter",
                Email = "patrick.schroeter@hs-augsburg.de",
                RzName = "schroeti",
                LdapId = 98765,
                Active = true,
                Created = DateTime.Now,
                EmployeeType = "Studenten"
            };
            newAppUser2.SetHashedPassword("safePassword");
            dbContext.AppUser.Add(newAppUser2);

            var newAppUser3 = new AppUser
            {
                Id = UserId3,
                Firstname = "Rainer",
                Lastname = "Unsinn",
                Email = "rainer.unsinn@spaß-am-leben.de",
                RzName = "runsi",
                LdapId = 85202,
                Active = false,
                Created = DateTime.Now,
                EmployeeType = "Studenten"
            };
            newAppUser3.SetHashedPassword("aBitOfAPassword");
            dbContext.AppUser.Add(newAppUser3);
        }
    }
}