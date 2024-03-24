using ENTOBEL_AURAVINA_API.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENTOBEL_AURAVINA_API.Domains
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
        }
    }
}
