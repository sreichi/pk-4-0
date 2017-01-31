using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class UserCreateDto
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string RzName { get; set; }

        public int LdapId { get; set; }

        public bool? Active { get; set; }
    }
}