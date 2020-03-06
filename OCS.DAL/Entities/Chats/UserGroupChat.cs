using OCS.DAL.Entities.Abstract;
using OCS.DAL.Entities.Users;
using System;

namespace OCS.DAL.Entities.Chats
{
    public class UserGroupChat : IBaseEntity<int>
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UserId { get; set; }

        public int GroupChatId { get; set; }

        public User User { get; set; }

        public GroupChat GroupChat { get; set; }
    }
}