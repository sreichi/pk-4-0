using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IStatusRepository
    {
        IEnumerable<Status> GetAllStatuses();
        Status GetStatusById(Guid status);
    }
}