using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FieldCreateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [NonEmptyGuid]
        public Guid FieldType { get; set; }
        public string Label { get; set; }
        public bool? Required { get; set; }
        public bool? MultipleSelect { get; set; }
        public string Value { get; set; }
        public string ContentType { get; set; }
        public string Placeholder { get; set; }
        public string OptionsJson { get; set; }
        public Guid? EnumOptionsTableId { get; set; }

        public virtual ICollection<Guid> StyleIds { get; set; }
        public virtual ICollection<Guid> ValidationIds { get; set; }
    }
}