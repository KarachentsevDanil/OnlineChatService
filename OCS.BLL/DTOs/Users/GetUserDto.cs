using System;

namespace OCS.BLL.DTOs.Users
{
    public class GetUserDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsOnline { get; set; }

        public DateTime LastSeenAt { get; set; }
    }
}