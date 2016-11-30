using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("role")]
    public partial class Role
    {
        public Role()
        {
            RolePermission = new HashSet<RolePermission>();
            UserHasRole = new HashSet<UserHasRole>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<RolePermission> RolePermission { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserHasRole> UserHasRole { get; set; }
    }
}
