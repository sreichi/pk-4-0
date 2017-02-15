using System;
using System.Collections.Generic;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FieldDefinitionDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<string> Configs { get; set; }
        public virtual IEnumerable<string> Styles { get; set; }
        public virtual IEnumerable<string> Validations { get; set; }
    }
}