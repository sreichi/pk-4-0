using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class UserDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string SaltString { get; set; }

        public int MatNr { get; set; }

        public int LdapId { get; set; }

        public bool? Active { get; set; }

        public DateTime Created { get; set; }

        public virtual IEnumerable<string> UserHasRole { get; set; }
    }
}