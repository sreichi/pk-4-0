using System;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ValidationDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ValidationString { get; set; }
    }
}