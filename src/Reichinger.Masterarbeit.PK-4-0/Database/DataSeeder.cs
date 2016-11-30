

using System;
using Microsoft.AspNetCore.Builder;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database
{
    public static class DataSeeder
    {
        static Guid _role1Id;
        static Guid _role2Id;
        static Guid _user1Id;
        static Guid _user2Id;
        static Guid _application1Id;
        static Guid _application2Id;
        static Guid _form1Id;
        static Guid _form2Id;
        static Guid _conference1Id;
        static Guid _conference2Id;
        static Guid _status1Id;
        static Guid _status2Id;
        static Guid _field1Id;
        static Guid _field2Id;
        static Guid _field3Id;
        static Guid _field4Id;
        static Guid _field5Id;
        static Guid _fieldType1Id;
        static Guid _fieldType2Id;
        static Guid _fieldType3Id;
        static Guid _fieldType4Id;
        static Guid _comment1Id;
        static Guid _comment2Id;
        static Guid _comment3Id;

        public static void SeedData(this IApplicationBuilder applicationBuilder)
        {
            _role1Id = Guid.NewGuid();
            _role2Id = Guid.NewGuid();
            _user1Id = Guid.NewGuid();
            _user2Id = Guid.NewGuid();
            _application1Id = Guid.NewGuid();
            _application2Id = Guid.NewGuid();
            _form1Id = Guid.NewGuid();
            _form2Id = Guid.NewGuid();
            _conference1Id = Guid.NewGuid();
            _conference2Id = Guid.NewGuid();
            _status1Id = Guid.NewGuid();
            _status2Id = Guid.NewGuid();
            _field1Id = Guid.NewGuid();
            _field2Id = Guid.NewGuid();
            _field3Id = Guid.NewGuid();
            _field4Id = Guid.NewGuid();
            _field5Id = Guid.NewGuid();
            _fieldType1Id = Guid.NewGuid();
            _fieldType2Id = Guid.NewGuid();
            _fieldType3Id = Guid.NewGuid();
            _fieldType4Id = Guid.NewGuid();
            _comment1Id = Guid.NewGuid();
            _comment2Id = Guid.NewGuid();
            _comment3Id = Guid.NewGuid();

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