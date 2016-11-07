using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Studiengang { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
