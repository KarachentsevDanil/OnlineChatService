using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCS.DAL.Entities.Chats;

namespace OCS.DAL.EF.Context.Mappings.Chats
{
    public static class GroupChatMapping
    {
        public static void MapGroupChat(this EntityTypeBuilder<GroupChat> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(250).IsRequired();

            builder.HasOne(p => p.Owner)
                .WithMany(p => p.OwnedGroups)
                .HasForeignKey(p => p.OwnerId);

            builder.HasQueryFilter(t => !t.IsDeleted);

            builder.ToTable("GroupChats", "chat");
        }
    }
}