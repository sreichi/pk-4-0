using System;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class UserListDto
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }
    }
}