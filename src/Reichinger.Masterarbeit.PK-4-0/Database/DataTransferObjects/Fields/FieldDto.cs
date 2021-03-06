﻿using System;
using System.Collections.Generic;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FieldDto
    {
        public string Name { get; set; }
        public Guid FieldType { get; set; }
        public string Label { get; set; }
        public bool? Required { get; set; }
        public bool? MultipleSelect { get; set; }
        public bool? Disabled { get; set; }
        public string Value { get; set; }
        public string ContentType { get; set; }
        public string Placeholder { get; set; }
        public string OptionsJson { get; set; }
        public Guid? EnumOptionsTableId { get; set; }

        public virtual IEnumerable<Guid> StyleIds { get; set; }
        public virtual IEnumerable<Guid> ValidationIds { get; set; }
    }
}