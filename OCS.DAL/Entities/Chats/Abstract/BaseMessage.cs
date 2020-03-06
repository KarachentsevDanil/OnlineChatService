using OCS.DAL.Entities.Abstract;
using OCS.DAL.Entities.Users;
using System;

namespace OCS.DAL.Entities.Chats.Abstract
{
    public abstract class BaseMessage : IBaseEntity<int>
    {
        public int Id { get; set; }

        public int ChatId { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public string Text { get; set; }

        public User CreatedByUser { get; set; }
    }
}