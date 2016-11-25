﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("form_has_field")]
    public partial class FormHasField
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("form_id")]
        public int FormId { get; set; }
        [Column("field_id")]
        public int FieldId { get; set; }

        [ForeignKey("FieldId")]
        [InverseProperty("FormHasField")]
        public virtual Field Field { get; set; }
        [ForeignKey("FormId")]
        [InverseProperty("FormHasField")]
        public virtual Form Form { get; set; }
    }
}
