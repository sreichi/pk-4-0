

using System;
using Microsoft.AspNetCore.Builder;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database
{
    public static class DataSeeder
    {
        public static readonly Guid RoleId1 = new Guid("8fa4497d-bee4-411d-83ef-f195037cbb43");
        public static readonly Guid RoleId2 = new Guid("c18ac352-9efc-4d64-8555-3379336309c8");

        public static readonly Guid UserId1 = new Guid("b904cc6e-b3a6-42a9-8880-3096be1b6c61");
        public static readonly Guid UserId2 = new Guid("ee632373-432e-40f0-9f33-8cc6b684e673");

        public static readonly Guid ApplicationId1 = new Guid("86c42368-ba33-4fca-a911-fa8d3758b01d");
        public static readonly Guid ApplicationId2 = new Guid("b630df84-32bd-42b5-a957-7f94a9ee503e");

        public static readonly Guid FormId1 = new Guid("bb2cf80b-6f7f-4305-8d65-4468908fd1f3");
        public static readonly Guid FormId2 = new Guid("e5253303-5f6e-474e-812b-d655afce5edb");

        public static readonly Guid ConferenceId1 = new Guid("74cf7b5c-1c7e-448b-ac5d-b9c63d466e1a");
        public static readonly Guid ConferenceId2 = new Guid("866ad5a9-64a4-4058-9751-b0dd27ef4d0e");

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

        public static void SeedData(this IApplicationBuilder applicationBuilder)
        {
            var db =
                applicationBuilder.ApplicationServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            CreateTestData(db);
        }

        private static void CreateTestData(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            CreateRoles(dbContext);
            CreateUsers(dbContext);
            CreateForms(dbContext);
            CreateStatuses(dbContext);
            CreateConferences(dbContext);
            CreateApplications(dbContext);
            CreateAssignments(dbContext);
            CreateComments(dbContext);
            CreateFieldTypes(dbContext);
            CreateFields(dbContext);
            CreateStyles(dbContext);
            CreateValidations(dbContext);
            CreateFormHasField(dbContext);
            CreateFieldHasStyle(dbContext);
            CreaFieldHasValidation(dbContext);
            CreateUserHasRole(dbContext);
            dbContext.SaveChanges();
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
                ValidationString = "max-lenght-20"
            });
            dbContext.Validation.Add(new Validation()
            {
                Id = ValidationId1,
                ValidationString = "min-length-8"
            });
        }

        private static void CreateStyles(ApplicationDbContext dbContext)
        {
            dbContext.Style.Add(new Style()
            {
                Id = StyleId1,
                StyleString = "small"
            });
            dbContext.Style.Add(new Style()
            {
                Id = StyleId2,
                StyleString = "alligned"
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
                FieldId = FieldId1
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = FormId1,
                FieldId = FieldId2
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = FormId1,
                FieldId = FieldId3
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = FormId1,
                FieldId = FieldId4
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
                Description = "INPUT",
                Name = "input"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = FieldTypeId2,
                Description = "CHECKBOX",
                Name = "checkbox"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = FieldTypeId3,
                Description = "RADIO",
                Name = "radio"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = FieldTypeId4,
                Description = "TEXTAREA",
                Name = "textarea"
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
                Text = "Da fehlt noch was",
                IsPrivate = false
            });
            dbContext.Comment.Add(new Comment
            {
                Id = CommentId2,
                ApplicationId = ApplicationId1,
                Created = DateTime.Parse("2016-10-18T13:15:08+00:00"),
                UserId = UserId2,
                Text = "Ja das Änder ich noch schnell",
                IsPrivate = false
            });
            dbContext.Comment.Add(new Comment
            {
                Id = CommentId3,
                ApplicationId = ApplicationId2,
                Created = DateTime.Parse("2016-11-18T15:15:08+00:00"),
                UserId = UserId1,
                Text = "Jetzt passt alles",
                IsPrivate = false
            });
        }

        private static void CreateConferences(ApplicationDbContext dbContext)
        {
            dbContext.Conference.Add(new Conference
            {
                Id = ConferenceId1,
                DateOfEvent = DateTime.Now,
                Description = "Hauptsitzung der PK"
            });
            dbContext.Conference.Add(new Conference
            {
                Id = ConferenceId2,
                DateOfEvent = DateTime.Now,
                Description = "Spontansitzung der Informatik"
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
                UserId = UserId1
            });
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = ApplicationId2,
                UserId = UserId2
            });
        }

        private static void CreateStatuses(ApplicationDbContext dbContext)
        {
            dbContext.Status.Add(
                new Status
                {
                    Id = StatusId1,
                    Name = "Active"
                });
            dbContext.Status.Add(
                new Status
                {
                    Id = StatusId2,
                    Name = "Updated"
                });
        }


        private static void CreateForms(ApplicationDbContext dbContext)
        {
            dbContext.Form.Add(
                new Form
                {
                    Id = FormId1,
                    Name = "Masterarbeit"
                });
            dbContext.Add(
                new Form
                {
                    Id = FormId2,
                    Name = "Notenanerkennung"
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
                    StatusId = StatusId2,
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
                    StatusId = StatusId1,
                    FormId = FormId1,
                    ConferenceId = ConferenceId1
                });
        }

        private static void CreateUsers(ApplicationDbContext dbContext)
        {
            dbContext.AppUser.Add(
                new AppUser
                {
                    Id = UserId1,
                    Firstname = "Stephan",
                    Lastname = "Reichinger",
                    Email = "stephan.reichinger@hs-augsburg.de",
                    Password =
                        "a336f671080fbf4f2a230f313560ddf0d0c12dfcf1741e49e8722a234673037dc493caa8d291d8025f71089d63cea809cc8ae53e5b17054806837dbe4099c4ca",
                    SaltString = "soSalty",
                    MatNr = 949223,
                    LdapId = 12345,
                    Active = true,
                    Created = DateTime.Now
                }
            );
            dbContext.AppUser.Add(
                new AppUser
                {
                    Id = UserId2,
                    Firstname = "Patrick",
                    Lastname = "Schröter",
                    Email = "patrick.schroeter@hs-augsburg.de",
                    Password =
                        "877a07cf4b7e1301aba8a5ce13caa61d06f4c2d3954f235c952797b44cccbc509e02a1c0482489ba76ec5ded767b1b010d34f05fc27f2fda115a35a9c023bbf3",
                    SaltString = "notSoSalty",
                    MatNr = 949225,
                    LdapId = 98765,
                    Active = false,
                    Created = DateTime.Now
                }
            );
        }
    }
}