using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("form")]
    public partial class Form
    {
        public Form()
        {
            Application = new HashSet<Application>();
            FormHasField = new HashSet<FormHasField>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar")]
        [MaxLength(50)]
        public string Title { get; set; }
        [Column("deprecated")]
        public bool Deprecated { get; set; }
        [Column("previous_version")]
        public Guid? PreviousVersion { get; set; }
        [Column("restricted_access")]
        public bool RestrictedAccess { get; set; }
        [Column("is_public")]
        public bool IsPublic { get; set; }

        [InverseProperty("Form")]
        public virtual ICollection<Application> Application { get; set; }
        [InverseProperty("Form")]
        public virtual ICollection<FormHasField> FormHasField { get; set; }
        [ForeignKey("PreviousVersion")]
        [InverseProperty("InversePreviousVersionNavigation")]
        public virtual Form PreviousVersionNavigation { get; set; }
        [InverseProperty("PreviousVersionNavigation")]
        public virtual ICollection<Form> InversePreviousVersionNavigation { get; set; }
    }
}
