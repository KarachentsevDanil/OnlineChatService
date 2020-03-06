namespace OCS.BLL.DTOs.Chats.Abstract
{
    public class BaseCreateChatMessageDto
    {
        public int ChatId { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }
    }
}