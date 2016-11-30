using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("role_permission")]
    public partial class RolePermission
    {
        [Column("role_id")]
        public Guid RoleId { get; set; }
        [Column("permission_id")]
        public Guid PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        [InverseProperty("RolePermission")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("RolePermission")]
        public virtual Role Role { get; set; }
    }
}
