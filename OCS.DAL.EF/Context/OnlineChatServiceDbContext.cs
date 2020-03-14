using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OCS.DAL.EF.Context.Mappings.Chats;
using OCS.DAL.EF.Context.Mappings.Users;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Users;
using OCS.DAL.Entities.Views.Chats;

namespace OCS.DAL.EF.Context
{
    public class OnlineChatServiceDbContext : IdentityDbContext<User>
    {
        public OnlineChatServiceDbContext(DbContextOptions<OnlineChatServiceDbContext> options) : base(options)
        {
        }

        public DbSet<UserContact> UserContacts { get; set; }

        public DbSet<GroupChat> GroupChats { get; set; }

        public DbSet<GroupChatMessage> GroupChatMessages { get; set; }

        public DbSet<PrivateChat> PrivateChats { get; set; }

        public DbSet<PrivateChatMessage> PrivateChatMessages { get; set; }

        public DbSet<UserGroupChat> UserGroupChats { get; set; }

        public DbSet<PrivateChatView> PrivateChatsView { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().MapUser();
            builder.Entity<UserContact>().MapUserContact();

            builder.Entity<PrivateChat>().MapPrivateChat();
            builder.Entity<PrivateChatMessage>().MapPrivateChatMessage();
            builder.Entity<GroupChat>().MapGroupChat();
            builder.Entity<GroupChatMessage>().MapGroupChatMessage();
            builder.Entity<UserGroupChat>().MapUserGroupChat();
            builder.Entity<PrivateChatView>().MapPrivateChatView();
        }
    }
}
