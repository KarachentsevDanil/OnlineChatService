namespace OCS.BLL.DTOs.Chats.Group
{
    public class AddUserToGroupChatDto
    {
        public int ChatId { get; set; }

        public string UserId { get; set; }

        public string AddedByUserId { get; set; }
    }
}