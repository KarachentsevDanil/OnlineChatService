using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCS.DAL.Entities.Chats;

namespace OCS.DAL.EF.Context.Mappings.Chats
{
    public static class PrivateChatMapping
    {
        public static void MapPrivateChat(this EntityTypeBuilder<PrivateChat> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(t => t.CreatedByUser)
                .WithMany(t => t.PrivateChats)
                .HasForeignKey(t => t.CreatedByUserId);

            builder.HasOne(t => t.InvitedUser)
                .WithMany()
                .HasForeignKey(t => t.InvitedUserId);

            builder.HasQueryFilter(t => !t.IsDeleted);

            builder.ToTable("PrivateChats", "chat");
        }
    }
}