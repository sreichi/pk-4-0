using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ConferenceDto<T>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DateOfEvent { get; set; }
        public virtual ICollection<T> Application { get; set; }
    }
}