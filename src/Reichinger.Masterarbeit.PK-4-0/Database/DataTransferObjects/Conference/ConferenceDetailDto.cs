using System;
using System.Collections.Generic;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ConferenceDetailDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string StartOfEvent { get; set; }
        public string EndOfEvent { get; set; }
        public string RoomOfEvent { get; set; }
        public int NumberOfConference { get; set; }
        public virtual IEnumerable<ApplicationListDto> Applications { get; set; }
        public virtual IEnumerable<UserListDto> Guests { get; set; }
        public virtual IEnumerable<UserListDto> Members { get; set; }
    }
}