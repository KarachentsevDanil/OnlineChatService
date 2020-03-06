using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCS.DAL.Entities.Chats;

namespace OCS.DAL.EF.Context.Mappings.Chats
{
    public static class UserGroupChatMapping
    {
        public static void MapUserGroupChat(this EntityTypeBuilder<UserGroupChat> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(t => t.User)
                .WithMany(t => t.GroupChats)
                .HasForeignKey(t => t.UserId);

            builder.HasOne(t => t.GroupChat)
                .WithMany(t => t.Users)
                .HasForeignKey(t => t.GroupChatId);

            builder.HasQueryFilter(t => !t.IsDeleted);

            builder.ToTable("UserGroupChats", "chat");
        }
    }
}