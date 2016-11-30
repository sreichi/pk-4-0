

using System;
using Microsoft.AspNetCore.Builder;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database
{
    public static class DataSeeder
    {
        static Guid _role1Id = new Guid("8fa4497d-bee4-411d-83ef-f195037cbb43");
        static Guid _role2Id = new Guid("c18ac352-9efc-4d64-8555-3379336309c8");
        static Guid _user1Id = new Guid("b904cc6e-b3a6-42a9-8880-3096be1b6c61");
        static Guid _user2Id = new Guid("ee632373-432e-40f0-9f33-8cc6b684e673");
        static Guid _application1Id = new Guid("86c42368-ba33-4fca-a911-fa8d3758b01d");
        static Guid _application2Id = new Guid("b630df84-32bd-42b5-a957-7f94a9ee503e");
        static Guid _form1Id = new Guid("bb2cf80b-6f7f-4305-8d65-4468908fd1f3");
        static Guid _form2Id = new Guid("e5253303-5f6e-474e-812b-d655afce5edb");
        static Guid _conference1Id = new Guid("74cf7b5c-1c7e-448b-ac5d-b9c63d466e1a");
        static Guid _conference2Id = new Guid("866ad5a9-64a4-4058-9751-b0dd27ef4d0e");
        static Guid _status1Id = new Guid("e3c1f89f-d9d5-4d76-a05a-2b3745d72c80");
        static Guid _status2Id = new Guid("ba72b0fb-9969-4942-801e-685b86059421");
        static Guid _field1Id = new Guid("3eeecb10-c634-444d-8619-e924df38cd3c");
        static Guid _field2Id = new Guid("7ac9cb82-f717-46e6-85e1-03e8783312a8");
        static Guid _field3Id = new Guid("1cf6b5ef-a891-4d56-8212-e04450616543");
        static Guid _field4Id = new Guid("5c46387f-613d-4f0d-bfec-52e9f7051cb0");
        static Guid _field5Id = new Guid("e1a7e22b-c482-41a5-806b-f9bc9d20891b");
        static Guid _fieldType1Id = new Guid("5c3914e9-a1ea-4c21-914a-39c2b5faa90c");
        static Guid _fieldType2Id = new Guid("77c630ab-8298-4847-a1f0-7cccc4172ea4");
        static Guid _fieldType3Id = new Guid("8abb2070-3abb-47b7-b50d-54b0d9b3db5d");
        static Guid _fieldType4Id = new Guid("3bd87eea-6b12-45c1-b3ec-0cb89f2f7115");
        static Guid _comment1Id = new Guid("df8452a4-de1d-4013-888c-4b486da7f269");
        static Guid _comment2Id = new Guid("4f8cd9b9-eb6a-4f56-b481-f35a1fd83493");
        static Guid _comment3Id = new Guid("ec243020-2158-4c26-87be-f3a83042fec1");

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
            CreateFormHasField(dbContext);
            CreateUserHasRole(dbContext);
            dbContext.SaveChanges();
        }


        private static void CreateRoles(ApplicationDbContext dbContext)
        {
            dbContext.Role.Add(
                new Role
                {
                    Id = _role1Id,
                    Name = "Admin"
                });
            dbContext.Role.Add(
                new Role
                {
                    Id = _role2Id,
                    Name = "User"
                });
        }

        private static void CreateUserHasRole(ApplicationDbContext dbContext)
        {
            dbContext.UserHasRole.Add(new UserHasRole
            {
                RoleId = _role1Id,
                UserId = _user1Id
            });
            dbContext.UserHasRole.Add(new UserHasRole
            {
                RoleId = _role2Id,
                UserId = _user1Id
            });
            dbContext.UserHasRole.Add(new UserHasRole
            {
                RoleId = _role1Id,
                UserId = _user2Id
            });
        }

        private static void CreateFormHasField(ApplicationDbContext dbContext)
        {
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = _form1Id,
                FieldId = _field1Id
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = _form1Id,
                FieldId = _field2Id
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = _form1Id,
                FieldId = _field3Id
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                FormId = _form1Id,
                FieldId = _field4Id
            });
        }

        private static void CreateFields(ApplicationDbContext dbContext)
        {
            dbContext.Field.Add(new Field
            {
                Id = _field1Id,
                FieldType = _fieldType1Id,
                Name = "Firstname"
            });
            dbContext.Field.Add(new Field
            {
                Id = _field2Id,
                FieldType = _fieldType1Id,
                Name = "Lastname"
            });
            dbContext.Field.Add(new Field
            {
                Id = _field3Id,
                FieldType = _fieldType3Id,
                Name = "Gender"
            });
            dbContext.Field.Add(new Field
            {
                Id = _field4Id,
                FieldType = _fieldType2Id,
                Name = "Active"
            });
            dbContext.Field.Add(new Field
            {
                Id = _field5Id,
                FieldType = _fieldType4Id,
                Name = "Beschreibung"
            });
        }

        private static void CreateFieldTypes(ApplicationDbContext dbContext)
        {
            dbContext.FieldType.Add(new FieldType
            {
                Id = _fieldType1Id,
                Description = "INPUT",
                Name = "input"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = _fieldType2Id,
                Description = "CHECKBOX",
                Name = "checkbox"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = _fieldType3Id,
                Description = "RADIO",
                Name = "radio"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = _fieldType4Id,
                Description = "TEXTAREA",
                Name = "textarea"
            });
        }

        private static void CreateComments(ApplicationDbContext dbContext)
        {
            dbContext.Comment.Add(new Comment
            {
                Id = _comment1Id,
                ApplicationId = _application1Id,
                Created = DateTime.Parse("2016-11-18T13:15:08+00:00"),
                UserId = _user1Id,
                Text = "Da fehlt noch was",
                IsPrivate = false
            });
            dbContext.Comment.Add(new Comment
            {
                Id = _comment2Id,
                ApplicationId = _application1Id,
                Created = DateTime.Parse("2016-10-18T13:15:08+00:00"),
                UserId = _user2Id,
                Text = "Ja das Änder ich noch schnell",
                IsPrivate = false
            });
            dbContext.Comment.Add(new Comment
            {
                Id = _comment3Id,
                ApplicationId = _application2Id,
                Created = DateTime.Parse("2016-11-18T15:15:08+00:00"),
                UserId = _user1Id,
                Text = "Jetzt passt alles",
                IsPrivate = false
            });
        }

        private static void CreateConferences(ApplicationDbContext dbContext)
        {
            dbContext.Conference.Add(new Conference
            {
                Id = _conference1Id,
                DateOfEvent = DateTime.Now,
                Description = "Hauptsitzung der PK"
            });
            dbContext.Conference.Add(new Conference
            {
                Id = _conference2Id,
                DateOfEvent = DateTime.Now,
                Description = "Spontansitzung der Informatik"
            });
        }

        private static void CreateAssignments(ApplicationDbContext dbContext)
        {
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = _application1Id,
                UserId = _user1Id
            });
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = _application1Id,
                UserId = _user2Id
            });
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = _application2Id,
                UserId = _user1Id
            });
            dbContext.Assignment.Add(new Assignment
            {
                ApplicationId = _application2Id,
                UserId = _user2Id
            });
        }

        private static void CreateStatuses(ApplicationDbContext dbContext)
        {
            dbContext.Status.Add(
                new Status
                {
                    Id = _status1Id,
                    Name = "Active"
                });
            dbContext.Status.Add(
                new Status
                {
                    Id = _status2Id,
                    Name = "Updated"
                });
        }


        private static void CreateForms(ApplicationDbContext dbContext)
        {
            dbContext.Form.Add(
                new Form
                {
                    Id = _form1Id,
                    Name = "Masterarbeit"
                });
            dbContext.Add(
                new Form
                {
                    Id = _form2Id,
                    Name = "Notenanerkennung"
                });
        }

        private static void CreateApplications(ApplicationDbContext dbContext)
        {
            dbContext.Application.Add(
                new Application
                {
                    Id = _application1Id,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    Version = 1,
                    IsCurrent = false,
                    PreviousVersion = null,
                    UserId = _user1Id,
                    StatusId = _status2Id,
                    FormId = _form1Id,
                    ConferenceId = _conference1Id
                });
            dbContext.Add(
                new Application
                {
                    Id = _application2Id,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    Version = 2,
                    IsCurrent = true,
                    PreviousVersion = _application1Id,
                    UserId = _user1Id,
                    StatusId = _status1Id,
                    FormId = _form1Id,
                    ConferenceId = _conference1Id
                });
        }

        private static void CreateUsers(ApplicationDbContext dbContext)
        {
            dbContext.AppUser.Add(
                new AppUser
                {
                    Id = _user1Id,
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
                    Id = _user2Id,
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