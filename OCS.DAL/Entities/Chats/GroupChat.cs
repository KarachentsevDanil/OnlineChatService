using OCS.DAL.Entities.Chats.Abstract;
using System.Collections.Generic;

namespace OCS.DAL.Entities.Chats
{
    public class GroupChat : BaseChat<GroupChatMessage>
    {
        public string Name { get; set; }

        public ICollection<UserGroupChat> Users { get; set; }
    }
}