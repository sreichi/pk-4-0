using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("user_has_role")]
    public partial class UserHasRole
    {
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("UserHasRole")]
        public virtual Role Role { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UserHasRole")]
        public virtual AppUser User { get; set; }
    }
}
