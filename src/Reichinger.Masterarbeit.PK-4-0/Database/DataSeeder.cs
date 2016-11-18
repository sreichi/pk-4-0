using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database
{
    public static class DataSeeder
    {
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
            CreateAsignees(dbContext);
            CreateComments(dbContext);
            CreateFieldTypes(dbContext);
            CreateFormFields(dbContext);
            CreateFormHasField(dbContext);
            CreateUserHasRole(dbContext);
            dbContext.SaveChanges();
        }

        private static void CreateUserHasRole(ApplicationDbContext dbContext)
        {
            dbContext.UserHasRole.Add(new UserHasRole
            {
                Id = 1,
                RoleId = 1,
                UserId = 1
            });
            dbContext.UserHasRole.Add(new UserHasRole
            {
                Id = 2,
                RoleId = 2,
                UserId = 1
            });
            dbContext.UserHasRole.Add(new UserHasRole
            {
                Id = 3,
                RoleId = 2,
                UserId = 2
            });
        }

        private static void CreateFormHasField(ApplicationDbContext dbContext)
        {
            dbContext.FormHasField.Add(new FormHasField
            {
                Id = 1,
                FormId = 1,
                FormFieldId = 1,
                Required = true,
                Label = "Firstname",
                PositionIndex = 1,
                Styling = "color:red,margin:5px"
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                Id = 2,
                FormId = 1,
                FormFieldId = 2,
                Required = true,
                Label = "Lastname",
                PositionIndex = 2,
                Styling = "color:green"
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                Id = 3,
                FormId = 1,
                FormFieldId = 3,
                Required = true,
                Label = "Geschlecht",
                PositionIndex = 3,
                Styling = "margin:5px"
            });
            dbContext.FormHasField.Add(new FormHasField
            {
                Id = 4,
                FormId = 1,
                FormFieldId = 4,
                Required = false,
                Label = "Zusatzinformationen",
                PositionIndex = 1,
                Styling = "padding:10px"
            });
        }

        private static void CreateFormFields(ApplicationDbContext dbContext)
        {
            dbContext.FormField.Add(new FormField
            {
                Id = 1,
                FieldType = 1,
                Name = "Firstname"
            });
            dbContext.FormField.Add(new FormField
            {
                Id = 2,
                FieldType = 1,
                Name = "Lastname"
            });
            dbContext.FormField.Add(new FormField
            {
                Id = 3,
                FieldType = 3,
                Name = "Gender"
            });
            dbContext.FormField.Add(new FormField
            {
                Id = 4,
                FieldType = 2,
                Name = "Active"
            });
            dbContext.FormField.Add(new FormField
            {
                Id = 5,
                FieldType = 4,
                Name = "Beschreibung"
            });
        }

        private static void CreateFieldTypes(ApplicationDbContext dbContext)
        {
            dbContext.FieldType.Add(new FieldType
            {
                Id = 1,
                Description = "INPUT"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = 2,
                Description = "CHECKBOX"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = 3,
                Description = "RADIO"
            });
            dbContext.FieldType.Add(new FieldType
            {
                Id = 4,
                Description = "TEXTAREA"
            });
        }

        private static void CreateComments(ApplicationDbContext dbContext)
        {
            dbContext.Comment.Add(new Comment
            {
                Id = 1,
                ApplicationId = 1,
                Created = DateTime.Parse("2016-11-18T13:15:08+00:00"),
                UserId = 1,
                Text = "Da fehlt noch was",
                IsPrivate = false
            });
            dbContext.Comment.Add(new Comment
            {
                Id = 2,
                ApplicationId = 1,
                Created = DateTime.Parse("2016-10-18T13:15:08+00:00"),
                UserId = 2,
                Text = "Ja das Änder ich noch schnell",
                IsPrivate = false
            });
            dbContext.Comment.Add(new Comment
            {
                Id = 3,
                ApplicationId = 2,
                Created = DateTime.Parse("2016-11-18T15:15:08+00:00"),
                UserId = 1,
                Text = "Jetzt passt alles",
                IsPrivate = false
            });
        }

        private static void CreateConferences(ApplicationDbContext dbContext)
        {
            dbContext.Conference.Add(new Conference
            {
                Id = 1,
                DateOfEvent = DateTime.Now,
                Description = "Hauptsitzung der PK"
            });
            dbContext.Conference.Add(new Conference
            {
                Id = 2,
                DateOfEvent = DateTime.Now,
                Description = "Spontansitzung der Informatik"
            });
        }

        private static void CreateAsignees(ApplicationDbContext dbContext)
        {
            dbContext.Asignee.Add(new Asignee
            {
                Id = 1,
                ApplicationId = 1,
                UserId = 1
            });
            dbContext.Asignee.Add(new Asignee
            {
                Id = 2,
                ApplicationId = 1,
                UserId = 2
            });
            dbContext.Asignee.Add(new Asignee
            {
                Id = 3,
                ApplicationId = 2,
                UserId = 1
            });
            dbContext.Asignee.Add(new Asignee
            {
                Id = 5,
                ApplicationId = 2,
                UserId = 2
            });
        }

        private static void CreateStatuses(ApplicationDbContext dbContext)
        {
            dbContext.Status.Add(
                new Status
                {
                    Id = 1,
                    Name = "Active"
                });
            dbContext.Status.Add(
                new Status
                {
                    Id = 2,
                    Name = "Updated"
                });
        }


        private static void CreateForms(ApplicationDbContext dbContext)
        {
            dbContext.Form.Add(
                new Form
                {
                    Id = 1,
                    Name = "Masterarbeit"
                });
            dbContext.Add(
                new Form
                {
                    Id = 2,
                    Name = "Notenanerkennung"
                });
        }

        private static void CreateApplications(ApplicationDbContext dbContext)
        {
            dbContext.Application.Add(
                new Application
                {
                    Id = 1,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    Version = 1,
                    IsCurrent = false,
                    PreviousVersion = 1,
                    UserId = 1,
                    StatusId = 2,
                    FormId = 1,
                    ConferenceId = 1
                });
            dbContext.Add(
                new Application
                {
                    Id = 2,
                    Created = DateTime.Now,
                    LastModified = DateTime.Now,
                    Version = 2,
                    IsCurrent = true,
                    PreviousVersion = 1,
                    UserId = 1,
                    StatusId = 1,
                    FormId = 1,
                    ConferenceId = 1
                });
        }

        private static void CreateUsers(ApplicationDbContext dbContext)
        {
            dbContext.AppUser.Add(
                new AppUser
                {
                    Id = 1,
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
                    Id = 2,
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

        private static void CreateRoles(ApplicationDbContext dbContext)
        {
            dbContext.Role.Add(
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                });
            dbContext.Role.Add(
                new Role
                {
                    Id = 2,
                    Name = "User"
                });
        }
    }
}