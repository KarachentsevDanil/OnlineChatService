using System;

namespace OCS.BLL.DTOs.Chats.Private
{
    public class GetPrivateChatViewDto
    {
        public int Id { get; set; }

        public string CreatedByUserId { get; set; }

        public string InvitedUserId { get; set; }

        public string CreatedByUserFullName { get; set; }

        public string CreatedByUserEmail { get; set; }

        public bool CreatedByUserIsOnline { get; set; }

        public string InvitedUserFullName { get; set; }

        public string InvitedUserEmail { get; set; }

        public bool InvitedUserIsOnline { get; set; }

        public string LastMessageText { get; set; }

        public string LastMessageCreatedByUserId { get; set; }

        public DateTime? LastMessageCreatedAt { get; set; }
    }
}