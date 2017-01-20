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
                UserId = response.UserId,
                ConferenceId = response.ConferenceId,
                StatusId = response.StatusId,
                FormId = response.FormId,
                Assignments = response.Assignment.Select(asignee => asignee.UserId),
                Comments = response.Comment.Select(comment => comment.ToDto()).OrderBy(dto => dto.Created)
            };
        }

        public static Application ToModel(this ApplicationCreateDto response)
        {
            return new Application()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                LastModified = DateTime.Now,
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
                Created = DateTime.Now,
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

        public static ConferenceDto<Guid> ToGuidDto(this Conference response)
        {
            var applications = new List<ApplicationDto>();
            response.Application.ToList().ForEach(application => applications.Add(application.ToDto()));

            return new ConferenceDto<Guid>()
            {
                Id = response.Id,
                DateOfEvent = response.DateOfEvent,
                Description = response.Description,
                Application = applications.Select(application => application.Id).ToImmutableList()
            };
        }

        public static ConferenceDto<ApplicationDto> ToFullDto(this Conference response)
        {
            var applications = new List<ApplicationDto>();
            response.Application.ToList().ForEach(application => applications.Add(application.ToDto()));

            return new ConferenceDto<ApplicationDto>()
            {
                Id = response.Id,
                DateOfEvent = response.DateOfEvent,
                Description = response.Description,
                Application = applications
            };
        }

        public static Conference ToModel(this ConferenceCreateDto response)
        {
            return new Conference()
            {
                Id = Guid.NewGuid(),
                DateOfEvent = response.DateOfEvent,
                Description = response.Description
            };
        }

        public static ConfigDto ToDto(this Config response)
        {
            return new ConfigDto()
            {
                Id = response.Id,
                Name = response.Name,
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

                FieldHasStyle = response.FieldHasStyle.Select(style => style.Style.StyleString),
                FieldHasValidation = response.FieldHasValidation.Select(validation => validation.Validation.ValidationString)
            };
        }

        public static FieldTypeDto ToDto(this FieldType response)
        {
            return new FieldTypeDto()
            {
                Id = response.Id,
                Name = response.Value,
                Description = response.Label,
                TypeHasConfig = response.TypeHasConfig.Select(config => config.Config.ToDto()),
                TypeHasStyle = response.TypeHasStyle.Select(style => style.Style.ToDto()),
                TypeHasValidation = response.TypeHasValidation.Select(validation => validation.Validation.ToDto())
            };
        }

        public static FormsDto ToDto(this Form response)
        {
            return new FormsDto()
            {
                Id =  response.Id,
                Name = response.Title,
                Application = response.Application.Select(e => e.Id),
                FormHasField = response.FormHasField.Select(field => field.FieldId)
            };
        }

        public static SingleFormDto ToSingleFormDto(this Form response)
        {
            return new SingleFormDto()
            {
                Id =  response.Id,
                Name = response.Title,
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
                Deprecated = false,
                IsPublic = response.IsPublic,
                RestrictedAccess = response.RestrictedAccess,
                PreviousVersion = null
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