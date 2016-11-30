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
                })
            };
        }
    }
}