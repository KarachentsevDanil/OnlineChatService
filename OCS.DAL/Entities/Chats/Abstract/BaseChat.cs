using OCS.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace OCS.DAL.Entities.Chats.Abstract
{
    public abstract class BaseChat<TMessage> : IBaseEntity<int>
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<TMessage> Messages { get; set; }
    }
}