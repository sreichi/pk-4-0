using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database
{
    public static class Mapper
    {
        public static ApplicationDto ToDto(this Application httpResponse)
        {
            return new ApplicationDto()
            {
                Id = httpResponse.Id,
                Created = httpResponse.Created,
                LastModified = httpResponse.LastModified,
                Version = httpResponse.Version,
                IsCurrent = httpResponse.IsCurrent,
                PreviousVersion = httpResponse.PreviousVersion ?? null,
                UserId = httpResponse.UserId,
                ConferenceId = httpResponse.ConferenceId,
                StatusId = httpResponse.StatusId,
                FormId = httpResponse.FormId,
                Assignments = httpResponse.Asignee.Select(asignee => asignee.UserId),
                Comments = httpResponse.Comment.Select(comment => new CommentDto()
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