using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reichinger.Masterarbeit.Database.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}
