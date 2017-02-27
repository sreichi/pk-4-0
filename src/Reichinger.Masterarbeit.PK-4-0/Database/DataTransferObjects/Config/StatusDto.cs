using System;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class StatusDto
    {
        public StatusValue Id { get; set; }
        public string Name { get; set; }
    }
}