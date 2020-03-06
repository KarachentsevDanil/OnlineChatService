using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCS.DAL.Entities.Chats;

namespace OCS.DAL.EF.Context.Mappings.Chats
{
    public static class PrivateChatMessageMapping
    {
        public static void MapPrivateChatMessage(this EntityTypeBuilder<PrivateChatMessage> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(t => t.CreatedByUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId);

            builder.HasOne(t => t.Chat)
                .WithMany(t => t.Messages)
                .HasForeignKey(t => t.ChatId);

            builder.Property(p => p.Text).HasMaxLength(1000).IsRequired();

            builder.HasQueryFilter(t => !t.IsDeleted);

            builder.ToTable("PrivateChatMessages", "chat");
        }
    }
}