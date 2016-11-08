using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("app_user")]
    public partial class AppUser
    {
        public AppUser()
        {
            Application = new HashSet<Application>();
            Asignee = new HashSet<Asignee>();
            Comment = new HashSet<Comment>();
            UserHasRole = new HashSet<UserHasRole>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("firstname", TypeName = "varchar")]
        [MaxLength(50)]
        public string Firstname { get; set; }
        [Required]
        [Column("lastname", TypeName = "varchar")]
        [MaxLength(50)]
        public string Lastname { get; set; }
        [Required]
        [Column("email", TypeName = "varchar")]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [Column("password", TypeName = "bpchar")]
        [MaxLength(128)]
        public string Password { get; set; }
        [Required]
        [Column("salt_string", TypeName = "varchar")]
        [MaxLength(50)]
        public string SaltString { get; set; }
        [Column("mat_nr")]
        public int MatNr { get; set; }
        [Column("ldap_id")]
        public int LdapId { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Application> Application { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Asignee> Asignee { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Comment> Comment { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserHasRole> UserHasRole { get; set; }
    }
}
