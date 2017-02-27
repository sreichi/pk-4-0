using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    public enum StatusValue
    {
        CREATED = 1, SUBMITTED = 2, RESCINDED = 3, PENDING = 4, DEACTIVATED = 5, ACCEPTED = 6, DENIED = 7
    }
}
