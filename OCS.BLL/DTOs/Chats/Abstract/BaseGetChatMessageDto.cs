using System;
using OCS.BLL.DTOs.Users;

namespace OCS.BLL.DTOs.Chats.Abstract
{
    public class BaseGetChatMessages
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public GetUserDto CreatedByUser { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}