using System.Collections.Immutable;

namespace OCS.BLL.DTOs.Chats.Abstract
{
    public class BaseGetChatMessagesDto<TMessage> 
        where TMessage : BaseGetChatMessages
    {
        public int Id { get; set; }

        public IImmutableList<BaseGetChatMessages> Messages { get; set; }
    }
}