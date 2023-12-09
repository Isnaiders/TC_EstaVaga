using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TCC_API.Models.EntitiesConfig.User
{
	public class UserConfig : IEntityTypeConfiguration<Entities.User.User>
	{
		public void Configure(EntityTypeBuilder<Entities.User.User> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).ValueGeneratedNever().IsRequired().HasColumnName("UserId");
		}
	}
}
