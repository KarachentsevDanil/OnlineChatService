using OCS.BLL.DTOs.Chats.Abstract;

namespace OCS.BLL.DTOs.Chats.Private
{
    public class GetPrivateChatMessageDto : BaseGetChatMessages
    {
        public GetPrivateChatDto Chat { get; set; }
    }
}