using System.Collections.Generic;
using OCS.BLL.DTOs.Chats.Abstract;
using OCS.DAL.Entities.Users;

namespace OCS.BLL.DTOs.Chats.Group
{
    public class GetGroupChatMessageDto : BaseGetChatMessages
    {
        public IEnumerable<User> Users { get; set; }
    }
}