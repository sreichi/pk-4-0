using System;
using System.Collections.Generic;
 using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
 using Reichinger.Masterarbeit.PK_4_0.Database.Models;

 namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
 {
     public interface IApplicationRepository
     {
         IEnumerable<ApplicationDto> GetAllApplications();
         ApplicationDto GetApplicationById(Guid applicationId);
         ApplicationDto CreateApplication(ApplicationCreateDto applicationToCreate);
         void Save();
     }
 }