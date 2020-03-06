using OCS.DAL.EF.Context;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Repositories.Contracts.Chats;

namespace OCS.DAL.EF.Repositories.Chats
{
    public class UserGroupChatRepository : GenericRepository<int, UserGroupChat, OnlineChatServiceDbContext>, IUserGroupChatRepository
    {
        public UserGroupChatRepository(OnlineChatServiceDbContext context) : base(context)
        {
        }
    }
}