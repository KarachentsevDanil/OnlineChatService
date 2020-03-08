using OCS.DAL.Entities.Chats.Abstract;
using System.Collections.Generic;
using OCS.DAL.Entities.Users;

namespace OCS.DAL.Entities.Chats
{
    public class GroupChat : BaseChat<GroupChatMessage>
    {
        public string Name { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<UserGroupChat> Users { get; set; }
    }
}