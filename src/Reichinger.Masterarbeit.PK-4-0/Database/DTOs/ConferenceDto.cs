using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ConferenceDto
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateOfEvent { get; set; }
        public virtual IEnumerable<int> Application { get; set; }
    }
}