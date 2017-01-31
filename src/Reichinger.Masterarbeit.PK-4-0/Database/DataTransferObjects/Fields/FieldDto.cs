using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FieldDto
    {
        public string Name { get; set; }
        public Guid FieldType { get; set; }
        public string Label { get; set; }
        public bool? Required { get; set; }
        public bool? MultipleSelect { get; set; }
        public string Value { get; set; }
        public string ContentType { get; set; }
        public string Placeholder { get; set; }
        public string Options { get; set; }
        public Guid? EnumOptionsTableId { get; set; }

        public virtual IEnumerable<string> FieldHasStyle { get; set; }
        public virtual IEnumerable<string> FieldHasValidation { get; set; }
    }
}