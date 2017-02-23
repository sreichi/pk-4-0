using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
 {
     public interface IApplicationRepository
     {
         IEnumerable<ApplicationListDto> GetAllApplications();
         IEnumerable<ApplicationListDto> GetAllApplicationsOfUser(Guid? userId);
         IEnumerable<ApplicationListDto> GetApplicationsOfUser(Guid applicationId, Guid userId);
         ApplicationDetailDto GetApplicationById(Guid applicationId);
         ApplicationDetailDto CreateApplication(ApplicationCreateDto applicationToCreate);
         CommentDto AddCommentToApplication(Guid applicationId, CommentCreateDto comment);
         IActionResult DeleteApplicationById(Guid applicationId);
         ApplicationDetailDto UpdateApplication(Guid applicationId, ApplicationCreateDto applicationPatch);
         IEnumerable<ApplicationDetailDto> GetHistoryOfApplication(Guid applicationId);
         CommentDto UpdateCommentOfApplication(Guid applicationId, Guid commentId, CommentCreateDto comment);
         IActionResult RemoveAssignmentFromApplication(Guid applicationId, Guid userId);
         IActionResult AssignUserToApplication(Guid applicationId, AssignmentCreateDto assignmentCreateDto);
         IActionResult UpdateStatusOfApplication(Guid applicationId, StatusDto statusDto);
         void Save();
     }
 }