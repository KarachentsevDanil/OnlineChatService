using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCS.DAL.Entities.Views.Chats;

namespace OCS.DAL.EF.Context.Mappings.Chats
{
    public static class PrivateChatViewMapping
    {
        public static void MapPrivateChatView(this EntityTypeBuilder<PrivateChatView> builder)
        {
            builder.HasNoKey();

            builder.ToView("PrivateChatView", "chat");
        }
    }
}