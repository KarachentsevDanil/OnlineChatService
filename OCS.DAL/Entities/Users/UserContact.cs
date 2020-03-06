using OCS.DAL.Entities.Abstract;
using System;

namespace OCS.DAL.Entities.Users
{
    public class UserContact : IBaseEntity<int>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string ContactId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }

        public User Contact { get; set; }
    }
}