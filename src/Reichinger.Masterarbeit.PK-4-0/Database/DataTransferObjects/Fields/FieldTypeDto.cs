using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FieldTypeDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<string> Configs { get; set; }
        public virtual IEnumerable<string> Styles { get; set; }
        public virtual IEnumerable<string> Validations { get; set; }
    }
}