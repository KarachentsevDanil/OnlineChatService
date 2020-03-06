using OCS.DAL.Entities.Chats.Abstract;

namespace OCS.DAL.Entities.Chats
{
    public class GroupChatMessage : BaseMessage
    {
        public GroupChat Chat { get; set; }
    }
}