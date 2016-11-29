using System.Collections.Generic;
 using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
 using Reichinger.Masterarbeit.PK_4_0.Database.Models;

 namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
 {
     public interface IApplicationRepository
     {
         IEnumerable<ApplicationDto> GetAllApplications();
         ApplicationDto GetApplicationById(int applicationId);
         ApplicationDto CreateApplication(ApplicationDto applicationToCreate);
         void Save();
     }
 }