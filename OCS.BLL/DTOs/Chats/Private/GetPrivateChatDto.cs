using OCS.BLL.DTOs.Users;

namespace OCS.BLL.DTOs.Chats.Private
{
    public class GetPrivateChatDto
    {
        public int Id { get; set; }

        public GetUserDto CreatedByUser { get; set; }

        public GetUserDto InvitedUser { get; set; }
    }
}