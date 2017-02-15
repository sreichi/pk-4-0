using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("app_user")]
    public partial class AppUser
    {
        public AppUser()
        {
            Application = new HashSet<Application>();
            Assignment = new HashSet<Assignment>();
            Comment = new HashSet<Comment>();
            UserHasRole = new HashSet<UserHasRole>();
        }

        [Column("id")]
        public Guid Id { get; set; }
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
        public string Password { get; private set; }
        [Required]
        [Column("salt_string", TypeName = "varchar")]
        [MaxLength(50)]
        public string SaltString { get; private set; }
        [Required]
        [Column("rz_name", TypeName = "varchar")]
        [MaxLength(50)]
        public string RzName { get; set; }
        [Required]
        [Column("employee_type", TypeName = "varchar")]
        [MaxLength(50)]
        public string EmployeeType { get; set; }
        [Column("ldap_id")]
        public int LdapId { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Application> Application { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Assignment> Assignment { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Attendant> Attendant { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Comment> Comment { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserHasRole> UserHasRole { get; set; }


        public void SetHashedPassword(string passwordInClearText)
        {
            if (string.IsNullOrEmpty(passwordInClearText))
                throw new ArgumentException("The password is not allowed to be null or empty.");

            var salt = GenerateSalt();
            Password = CreatePasswordHash(passwordInClearText, salt);
            SaltString = Convert.ToBase64String(salt);
        }

        public string CreatePasswordHash(string unhashedPassword, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: unhashedPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}
