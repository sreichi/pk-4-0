using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<Status> _dbStatuses;

        public StatusRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbStatuses = _applicationDbContext.Status;
        }
        public IEnumerable<Status> GetAllStatuses()
        {
            return _dbStatuses.ToList();
        }

        public Status GetStatusById(Guid statusId)
        {
            var status = _dbStatuses.FirstOrDefault(entry => entry.Id == statusId);
            return status;
        }
    }
}
