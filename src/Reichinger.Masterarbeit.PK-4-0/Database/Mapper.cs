using System;
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
                UserId = response.UserId,
                ConferenceId = response.ConferenceId,
                StatusId = response.StatusId,
                FormId = response.FormId,
                Assignments = response.Assignment.Select(asignee => asignee.UserId),
                Comments = response.Comment.Select(comment => comment.ToDto()).OrderBy(dto => dto.Created)
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
                Text = response.Text,
                ApplicationId = response.ApplicationId,
                UserId = response.UserId
            };
        }

        public static CommentDto ToDto(this Comment response)
        {
            return new CommentDto()
            {
                Created = response.Created,
                IsPrivate = response.IsPrivate,
                RequiresChanges = response.RequiresChanges,
                Text = response.Text,
                ApplicationId = response.ApplicationId,
                UserId = response.UserId
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

        public static FormsDto ToDto(this Form response)
        {
            return new FormsDto()
            {
                Id =  response.Id,
                Name = response.Name,
                Application = response.Application.Select(e => e.Id),
                FormHasField = response.FormHasField.Select(field => field.FieldId)
            };
        }

        public static SingleFormDto ToSingleFormDto(this Form response)
        {
            return new SingleFormDto()
            {
                Id =  response.Id,
                Name = response.Name,
                Application = response.Application.Select(e => e.Id),
                FormHasField = response.FormHasField.Select(field => field.Field.ToDto())
            };
        }

        public static Form ToModel(this FormCreateDto response)
        {
            return new Form()
            {
                Id = Guid.NewGuid(),
                Name = response.Name,
                Deprecated = false,
                IsPublic = response.IsPublic,
                RestrictedAccess = response.RestrictedAccess,
                PreviousVersion = null
            };
        }
    }
}