using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        public IEnumerable<Application> GetAllApplications()
        {
            throw new System.NotImplementedException();
        }

        public Application GetApplicationById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}