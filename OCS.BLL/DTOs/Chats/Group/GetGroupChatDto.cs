using System.Collections.Immutable;
using OCS.BLL.DTOs.Users;

namespace OCS.BLL.DTOs.Chats.Group
{
    public class GetGroupChatDto
    {
        public int Id { get; set; }

        public GetUserDto Owner { get; set; }

        public IImmutableList<GetUserDto> Users { get; set; }
    }
}