using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OCS.DAL.Entities.Users;

namespace OCS.DAL.EF.Context.Mappings.Users
{
    public static class UserMapping
    {
        public static void MapUser(this EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FirstName).HasMaxLength(250).IsRequired();

            builder.Property(p => p.LastName).HasMaxLength(250).IsRequired();

            builder.HasQueryFilter(t => !t.IsDeleted);

            builder.ToTable("Users", "user");
        }
    }
}