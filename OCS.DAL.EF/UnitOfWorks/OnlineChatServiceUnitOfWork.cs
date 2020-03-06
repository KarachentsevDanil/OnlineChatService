using OCS.DAL.EF.Context;
using OCS.DAL.EF.Repositories.Chats;
using OCS.DAL.EF.Repositories.Users;
using OCS.DAL.Repositories.Contracts.Chats;
using OCS.DAL.Repositories.Contracts.Users;
using OCS.DAL.UnitOfWorks.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.DAL.EF.UnitOfWorks
{
    public class OnlineChatServiceUnitOfWork : IUnitOfWork
    {
        private readonly OnlineChatServiceDbContext _context;

        private IGroupChatMessageRepository _groupChatMessageRepository;

        private IGroupChatRepository _groupChatRepository;

        private IPrivateChatMessageRepository _privateChatMessageRepository;

        private IPrivateChatRepository _privateChatRepository;

        private IUserGroupChatRepository _userGroupChatRepository;

        private IUserRepository _userRepository;

        private IUserContactRepository _userContactRepository;

        public OnlineChatServiceUnitOfWork(OnlineChatServiceDbContext context)
        {
            _context = context;
        }

        public IGroupChatMessageRepository GroupChatMessageRepository => _groupChatMessageRepository ?? new GroupChatMessageRepository(_context);

        public IGroupChatRepository GroupChatRepository => _groupChatRepository ?? new GroupChatRepository(_context);

        public IPrivateChatMessageRepository PrivateChatMessageRepository => _privateChatMessageRepository ?? new PrivateChatMessageRepository(_context);

        public IPrivateChatRepository PrivateChatRepository => _privateChatRepository ?? new PrivateChatRepository(_context);

        public IUserGroupChatRepository UserGroupChatRepository => _userGroupChatRepository ?? new UserGroupChatRepository(_context);

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);

        public IUserContactRepository UserContactRepository => _userContactRepository ?? new UserContactRepository(_context);

        public async Task CommitAsync(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}