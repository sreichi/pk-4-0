using System;
using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database
{
    // This class contains all Mappings
    // it is possible to map enitities to dtos and dtos back to entities
    public static class Mapper
    {
        public static ApplicationDetailDto ToDetailDto(this Application response)
        {
            return new ApplicationDetailDto()
            {
                Id = response.Id,
                Created = response.Created,
                LastModified = response.LastModified,
                Version = response.Version,
                IsCurrent = response.IsCurrent,
                PreviousVersion = response.PreviousVersion ?? null,
                FilledForm = response.FilledForm,
                User = response.User?.ToDetailDto(),
                Conference = response.Conference?.ToListDto(),
                StatusId = response.StatusId,
                Form = response.Form?.ToDetailDto(),
                Assignments = response.Assignment?.Select(asignee => asignee.User.ToDetailDto()),
                Comments = response.Comment?.Select(comment => comment.ToDto()).OrderBy(dto => dto.Created) ?? null
            };
        }

        public static ApplicationListDto ToListDto(this Application response)
        {
            return new ApplicationListDto()
            {
                Id = response.Id,
                Created = response.Created,
                LastModified = response.LastModified,
                FilledForm = response.FilledForm,
                IsCurrent = response.IsCurrent,
                Version = response.Version,
                User = response.User.ToDetailDto(),
                Conference = response.Conference?.ToListDto(),
                StatusId = response.StatusId,
                Form = response.Form.ToListDto(),
            };
        }

        public static Application ToModel(this ApplicationCreateDto response)
        {
            return new Application()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                FilledForm = response.FilledForm,
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
                Message = response.Message,
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
                Message = response.Message,
                User = response.User.ToDetailDto()
            };
        }


        public static CommentCreateDto ToCreateDto(this Comment response)
        {
            return new CommentCreateDto()
            {
                IsPrivate = response.IsPrivate,
                Message = response.Message,
                RequiresChanges = response.RequiresChanges,
                UserId = response.UserId
            };
        }

        public static ConferenceListDto ToListDto(this Conference response)
        {
            return new ConferenceListDto()
            {
                Id = response.Id,
                DateOfEvent = response.DateOfEvent,
                Description = response.Description,
                StartOfEvent = response.StartOfEvent,
                EndOfEvent = response.EndOfEvent,
                RoomOfEvent = response.RoomOfEvent,
                NumberOfConference = response.NumberOfConference
            };
        }

        public static ConferenceDetailDto ToDetailDto(this Conference response)
        {

            return new ConferenceDetailDto()
            {
                Id = response.Id,
                DateOfEvent = response.DateOfEvent,
                Description = response.Description,
                StartOfEvent = response.StartOfEvent,
                EndOfEvent = response.EndOfEvent,
                RoomOfEvent = response.RoomOfEvent,
                NumberOfConference = response.NumberOfConference,
                ConfigJson = response.ConferenceConfiguration,
                Applications = response.Application.Select(application => application.ToListDto()),
                Guests = response.Attendant.Where(attendant => attendant.TypeOfAttendance == TypeOfAttendance.GUEST).Select(attendant => attendant.User.ToListDto()),
                Members = response.Attendant.Where(attendant => attendant.TypeOfAttendance == TypeOfAttendance.MEMBER).Select(attendant => attendant.User.ToListDto())
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
                NumberOfConference = response.NumberOfConference,
                ConferenceConfiguration = response.ConfigJson
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
                OptionsJson = response.Options,
                MultipleSelect = response.MultipleSelect,
                Disabled = response.Disabled,
                EnumOptionsTableId = response.EnumOptionsTableId,

                StyleIds = response.FieldHasStyle.Select(style => style.Style.Id),
                ValidationIds =
                    response.FieldHasValidation.Select(validation => validation.Validation.Id)
            };
        }

        public static FieldDefinitionDto ToDto(this FieldType response)
        {
            return new FieldDefinitionDto()
            {
                Id = response.Id,
                Name = response.Value,
                Description = response.Label,
                Configs = response.TypeHasConfig.OrderBy(config => config.Position).Select(config => config.Config.Value),
                StyleIds = response.TypeHasStyle.Select(style => style.Style.Id),
                ValidationIds = response.TypeHasValidation.Select(validation => validation.Validation.Id)
            };
        }

        public static FormListDto ToListDto(this Form response)
        {
            return new FormListDto()
            {
                Id = response.Id,
                Title = response.Title,
                Created = response.Created,
                Deprecated = response.Deprecated,
                IsActive = response.IsActive,
                IsPublic = response.IsPublic,
                RestrictedAccess = response.RestrictedAccess
            };
        }

        public static FormDetailDto ToDetailDto(this Form response)
        {
            return new FormDetailDto()
            {
                Id = response.Id,
                Title = response.Title,
                Created = response.Created,
                Deprecated = response.Deprecated,
                IsActive = response.IsActive,
                IsPublic = response.IsPublic,
                RestrictedAccess = response.RestrictedAccess,
                FormHasField = response.FormHasField.OrderBy(field => field.Position).Select(field => field.Field.ToDto())
            };
        }

        public static Form ToModel(this FormCreateDto response)
        {
            return new Form()
            {
                Id = Guid.NewGuid(),
                Title = response.Title,
                Created = DateTime.UtcNow,
                Deprecated = false,
                IsPublic = response.IsPublic,
                RestrictedAccess = response.RestrictedAccess
            };
        }

        public static PermissionDto ToDto(this Permission response)
        {
            return new PermissionDto()
            {
                Id = response.Id,
                Name = response.Name
            };
        }

        public static RoleDto ToDto(this Role response)
        {
            return new RoleDto()
            {
                Id = response.Id,
                Name = response.Name
            };
        }

        public static Role ToModel(this RoleDto response)
        {
            return new Role()
            {
                Id = Guid.NewGuid(),
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

        public static UserDetailDto ToDetailDto(this AppUser response)
        {
            return new UserDetailDto()
            {
                Id = response.Id,
                Firstname = response.Firstname,
                Lastname = response.Lastname,
                Created = response.Created,
                Active = response.Active,
                Email = response.Email,
                LdapId = response.LdapId,
                RzName = response.RzName,
                EmployeeType = response.EmployeeType,
                Roles = response.UserHasRole.Select(e => e.Role.ToDto())
            };
        }

        public static UserListDto ToListDto(this AppUser response)
        {
            return new UserListDto()
            {
                Id = response.Id,
                Firstname = response.Firstname,
                Lastname = response.Lastname,
                Email = response.Email,
            };
        }

        public static AppUser ToModel(this UserCreateDto response)
        {
            var newAppUser = new AppUser()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Email = response.Email,
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