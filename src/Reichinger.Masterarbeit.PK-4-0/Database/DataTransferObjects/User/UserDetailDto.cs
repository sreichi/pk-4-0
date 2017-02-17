using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class UserDetailDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        public string RzName { get; set; }

        public int LdapId { get; set; }

        public string EmployeeType { get; set; }

        public bool? Active { get; set; }

        public DateTime Created { get; set; }

        public virtual IEnumerable<RoleDto> Roles { get; set; }
    }
}