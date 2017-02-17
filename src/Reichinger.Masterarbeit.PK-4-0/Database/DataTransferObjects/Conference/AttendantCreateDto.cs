using System;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class AttendantCreateDto
    {
        public Guid UserId { get; set; }
        public TypeOfAttendance TypeOfAttendance { get; set; }
    }
}