using System;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

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
                Comments = response.Comment.Select(comment => new CommentDto()
                {
                    UserId = comment.UserId,
                    ApplicationId = comment.ApplicationId,
                    Created = comment.Created,
                    IsPrivate = comment.IsPrivate,
                    RequiresChanges = comment.RequiresChanges,
                    Text = comment.Text
                }).OrderBy(dto => dto.Created)
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

        public static Field ToModel(this FieldCreateDto response, Guid )
        {
            return new FormHasField()
            {
                FormId = newForm.Id,
                Field = new Field()
                {
                    Id = newFormFieldÍd,
                    Name = field.Name,
                    FieldType = field.FieldType,
                    Label = field.Label ?? null,
                    MultipleSelect = field.MultipleSelect ?? null,
                    Required = field.Required ?? null,
                    Placeholder = field.Placeholder ?? null,
                    ContentType = field.ContentType,
                    Value = field.Value ?? null,
                    Options = field.Options ?? null,
                    EnumOptionsTableId = field.EnumOptionsTableId ?? null
                }
            }
        }

        public static FormDto ToDto(this Form response)
        {
            return new FormDto()
            {
                Id =  response.Id,
                Name = response.Name,
                Application = response.Application.Select(e => e.Id),
                FormHasField = response.FormHasField.Select(field => field.FieldId)
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