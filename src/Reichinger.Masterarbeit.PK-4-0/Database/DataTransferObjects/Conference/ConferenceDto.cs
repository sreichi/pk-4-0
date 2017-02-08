using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ConferenceDto<TApplicationType, TAttendandType>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DateOfEvent { get; set; }
        public DateTime StartOfEvent { get; set; }
        public DateTime EndOfEvent { get; set; }
        public string RoomOfEvent { get; set; }
        public int NumberOfConference { get; set; }
        public virtual IEnumerable<TApplicationType> Application { get; set; }
        public virtual IEnumerable<TAttendandType> Attendand { get; set; }
    }
}