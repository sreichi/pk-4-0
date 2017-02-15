using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
 using Reichinger.Masterarbeit.PK_4_0.Database.Models;

 namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
 {
     public interface IApplicationRepository
     {
         IEnumerable<ApplicationListDto> GetAllApplications();
         ApplicationDetailDto GetApplicationById(Guid applicationId);
         ApplicationDetailDto CreateApplication(ApplicationCreateDto applicationToCreate);
         CommentDto AddCommentToApplication(Guid applicationId, CommentCreateDto comment);
         IActionResult DeleteApplicationById(Guid applicationId);
         ApplicationDetailDto UpdateApplication(Guid applicationId, ApplicationCreateDto applicationPatch);
         IEnumerable<ApplicationDetailDto> GetHistoryOfApplication(Guid applicationId);
         CommentDto UpdateCommentOfApplication(Guid applicationId, Guid commentId, CommentCreateDto comment);
         void Save();
     }
 }