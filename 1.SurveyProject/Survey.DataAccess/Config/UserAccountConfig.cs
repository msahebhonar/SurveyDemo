using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Entities.User;

namespace Survey.DataAccess.Config
{
    public class UserAccountConfig : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder
                .Ignore(x => x.Fullname);

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(50);

            builder
                .Property(x => x.LastName)
                .HasMaxLength(100);

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(320);

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
