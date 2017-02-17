using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ConferenceListDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string StartOfEvent { get; set; }
        public string EndOfEvent { get; set; }
        public string RoomOfEvent { get; set; }
        public int NumberOfConference { get; set; }
    }
}