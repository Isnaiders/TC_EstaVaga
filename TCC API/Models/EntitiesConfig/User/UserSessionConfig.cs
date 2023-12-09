using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TCC_API.Models.EntitiesConfig.User
{
    public class UserSessionConfig : IEntityTypeConfiguration<Entities.User.UserSession>
    {
        public void Configure(EntityTypeBuilder<Entities.User.UserSession> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever().IsRequired().HasColumnName("UserSessionId");
            //builder.HasOne(d => d.User)
            //        .WithMany(p => p.Session)
            //        .HasForeignKey(d => d.UserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_dbo.UserSession_dbo.User_UserId");
        }
    }
}
