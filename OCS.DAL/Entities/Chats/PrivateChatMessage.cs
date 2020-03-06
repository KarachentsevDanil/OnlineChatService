using OCS.DAL.Entities.Chats.Abstract;

namespace OCS.DAL.Entities.Chats
{
    public class PrivateChatMessage : BaseMessage
    {
        public PrivateChat Chat { get; set; }
    }
}