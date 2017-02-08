using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Swashbuckle.Swagger.Model;

namespace Reichinger.Masterarbeit.PK_4_0.Database
{
    public static class Mapper
    {
        public static ApplicationDto ToDto(this Application response)
        {
            return new ApplicationDto()
            {
                Id = response.Id,
                Created = response.Created,
                LastModified = response.LastModified,
                Version = response.Version,
                IsCurrent = response.IsCurrent,
                PreviousVersion = response.PreviousVersion ?? null,
                FilledForm = response.FilledForm,
                User = response.User.ToDto(),
                ConferenceId = response.ConferenceId,
                Status = response.Status.ToDto(),
                FormId = response.FormId,
                Assignments = response.Assignment.Select(asignee => asignee.UserId),
                Comments = response.Comment.Select(comment => comment.ToDto()).OrderBy(dto => dto.Created) ?? null
            };
        }

        public static Application ToModel(this ApplicationCreateDto response)
        {
            return new Application()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Version = response.Version,
                IsCurrent = response.IsCurrent,
                FilledForm = response.FilledForm,
                PreviousVersion = response.PreviousVersion ?? null,
                UserId = response.UserId,
                ConferenceId = response.ConferenceId,
                StatusId = response.StatusId,
                FormId = response.FormId
            };
        }

        public static Comment ToModel(this CommentCreateDto response)
        {
            return new Comment()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                IsPrivate = response.IsPrivate,
                RequiresChanges = response.RequiresChanges,
                Message = response.Text,
                UserId = response.UserId
            };
        }

        public static CommentDto ToDto(this Comment response)
        {
            return new CommentDto()
            {
                Id = response.Id,
                Created = response.Created,
                IsPrivate = response.IsPrivate,
                RequiresChanges = response.RequiresChanges,
                Text = response.Message,
                ApplicationId = response.ApplicationId,
                UserId = response.UserId
            };
        }

        public static ConferenceDto<Guid, Guid> ToGuidDto(this Conference response)
        {
            var applications = new List<ApplicationDto>();
            response.Application.ToList().ForEach(application => applications.Add(application.ToDto()));

            return new ConferenceDto<Guid, Guid>()
            {
                Id = response.Id,
                DateOfEvent = response.DateOfEvent,
                Description = response.Description,
                StartOfEvent = response.StartOfEvent,
                EndOfEvent = response.EndOfEvent,
                RoomOfEvent = response.RoomOfEvent,
                NumberOfConference = response.NumberOfConference,
                Application = applications.Select(application => application.Id).ToImmutableList(),
                Attendand = response.Attendand.Select(attendand => attendand.UserId)
            };
        }

        public static ConferenceDto<ApplicationDto, UserDto> ToFullDto(this Conference response)
        {
            var applications = new List<ApplicationDto>();
            response.Application.ToList().ForEach(application => applications.Add(application.ToDto()));

            return new ConferenceDto<ApplicationDto, UserDto>()
            {
                Id = response.Id,
                DateOfEvent = response.DateOfEvent,
                Description = response.Description,
                StartOfEvent = response.StartOfEvent,
                EndOfEvent = response.EndOfEvent,
                RoomOfEvent = response.RoomOfEvent,
                NumberOfConference = response.NumberOfConference,
                Application = applications,
                Attendand = response.Attendand.Select(attendand => attendand.User.ToDto())
            };
        }

        public static Conference ToModel(this ConferenceCreateDto response)
        {
            return new Conference()
            {
                Id = Guid.NewGuid(),
                DateOfEvent = response.DateOfEvent,
                Description = response.Description,
                StartOfEvent = response.StartOfEvent,
                EndOfEvent = response.EndOfEvent,
                RoomOfEvent = response.RoomOfEvent,
                NumberOfConference = response.NumberOfConference
            };
        }

        public static ConfigDto ToDto(this Config response)
        {
            return new ConfigDto()
            {
                Id = response.Id,
                Value = response.Value
            };
        }

        public static FieldDto ToDto(this Field response)
        {
            return new FieldDto()
            {
                Name = response.Name,
                Label = response.Label,
                ContentType = response.ContentType,
                FieldType = response.FieldType,
                Placeholder = response.Placeholder,
                Value = response.Value,
                Required = response.Required,
                Options = response.Options,
                MultipleSelect = response.MultipleSelect,
                EnumOptionsTableId = response.EnumOptionsTableId,

                Styles = response.FieldHasStyle.Select(style => style.Style.StyleString),
                Validations =
                    response.FieldHasValidation.Select(validation => validation.Validation.ValidationString)
            };
        }

        public static FieldTypeDto ToDto(this FieldType response)
        {
            return new FieldTypeDto()
            {
                Id = response.Id,
                Name = response.Value,
                Description = response.Label,
                TypeHasConfig = response.TypeHasConfig.OrderBy(config => config.Position).Select(config => config.ToDto()),
                TypeHasStyle = response.TypeHasStyle.Select(style => style.Style.ToDto()),
                TypeHasValidation = response.TypeHasValidation.Select(validation => validation.Validation.ToDto())
            };
        }

        public static FormsDto ToDto(this Form response)
        {
            return new FormsDto()
            {
                Id = response.Id,
                Name = response.Title,
                Created = response.Created,
                Application = response.Application.Select(e => e.Id),
                FormHasField = response.FormHasField.Select(field => field.FieldId)
            };
        }

        public static SingleFormDto ToSingleFormDto(this Form response)
        {
            return new SingleFormDto()
            {
                Id = response.Id,
                Name = response.Title,
                Created = response.Created,
                Application = response.Application.Select(e => e.Id),
                FormHasField = response.FormHasField.Select(field => field.Field.ToDto())
            };
        }

        public static Form ToModel(this FormCreateDto response)
        {
            return new Form()
            {
                Id = Guid.NewGuid(),
                Title = response.Name,
                Created = DateTime.UtcNow,
                Deprecated = false,
                IsPublic = response.IsPublic,
                RestrictedAccess = response.RestrictedAccess,
                PreviousVersion = null
            };
        }

        public static RoleDto ToDto(this Role response)
        {
            return new RoleDto()
            {
                Id = response.Id,
                Name = response.Name,
                UserHasRole = response.UserHasRole.Select(role => role.UserId)
            };
        }

        public static StatusDto ToDto(this Status response)
        {
            return new StatusDto()
            {
                Id = response.Id,
                Name = response.Name
            };
        }

        public static StyleDto ToDto(this Style response)
        {
            return new StyleDto()
            {
                Id = response.Id,
                Description = response.Description,
                StyleString = response.StyleString
            };
        }

        public static TypeHasConfigDto ToDto(this TypeHasConfig response)
        {
            return new TypeHasConfigDto()
            {
                ConfigJSON = response.Config.Value
            };
        }

        public static UserDto ToDto(this AppUser response)
        {
            return new UserDto()
            {
                Id = response.Id,
                Firstname = response.Firstname,
                Lastname = response.Lastname,
                Created = response.Created,
                Active = response.Active,
                Email = response.Email,
                LdapId = response.LdapId,
                RzName = response.RzName,
                UserHasRole = response.UserHasRole.Select(e => e.Role.Name)
            };
        }

        public static AppUser ToModel(this UserCreateDto response)
        {
            var newAppUser = new AppUser()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Firstname = response.Firstname,
                Lastname = response.Lastname,
                Email = response.Email,
                LdapId = response.LdapId,
                RzName = response.RzName,
                Active = true
            };
            newAppUser.SetHashedPassword(response.Password);
            return newAppUser;
        }

        public static ValidationDto ToDto(this Validation response)
        {
            return new ValidationDto()
            {
                Id = response.Id,
                Description = response.Description,
                ValidationString = response.ValidationString
            };
        }
    }
}