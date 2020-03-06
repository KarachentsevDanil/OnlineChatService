using OCS.DAL.Repositories.Contracts.Chats;
using OCS.DAL.Repositories.Contracts.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.UnitOfWorks.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGroupChatMessageRepository GroupChatMessageRepository { get; }

        IGroupChatRepository GroupChatRepository { get; }

        IPrivateChatMessageRepository PrivateChatMessageRepository { get; }

        IPrivateChatRepository PrivateChatRepository { get; }

        IUserGroupChatRepository UserGroupChatRepository { get; }

        IUserRepository UserRepository { get; }

        IUserContactRepository UserContactRepository { get; }

        Task CommitAsync(CancellationToken token = default);
    }
}
