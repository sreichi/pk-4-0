using System;
using System.ComponentModel.DataAnnotations;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ConferenceCreateDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateOfEvent { get; set; }
        public string StartOfEvent { get; set; }
        public string EndOfEvent { get; set; }
        public string RoomOfEvent { get; set; }
        public int NumberOfConference { get; set; }
        public string ConfigJson { get; set; }
    }
}