using OCS.DAL.Entities.Chats.Abstract;
using OCS.DAL.Entities.Users;

namespace OCS.DAL.Entities.Chats
{
    public class PrivateChat : BaseChat<PrivateChatMessage>
    {
        public string CreatedByUserId { get; set; }

        public string InvitedUserId { get; set; }

        public User CreatedByUser { get; set; }

        public User InvitedUser { get; set; }
    }
}