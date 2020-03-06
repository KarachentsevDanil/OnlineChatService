using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCS.DAL.Entities.Users;

namespace OCS.DAL.EF.Context.Mappings.Users
{
    public static class UserContactMapping
    {
        public static void MapUserContact(this EntityTypeBuilder<UserContact> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(t => t.Contact)
                .WithMany(t => t.Contacts)
                .HasForeignKey(t => t.ContactId);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            builder.HasQueryFilter(t => !t.IsDeleted);

            builder.ToTable("UserContacts", "user");
        }
    }
}