using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UM.Domain;

namespace Common.Persistence.EntityTypeConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(u => u.ApplicationUserId);
        builder.Property(x => x.ApplicationUserId).HasDefaultValueSql("NEWID()");
        
        builder.Property(u => u.Login).IsUnicode().HasMaxLength(320);

        builder
            .HasMany(u => u.ApplicationUserRoles)
            .WithMany(u => u.ApplicationUsers);

        builder.Navigation(u => u.ApplicationUserRoles).AutoInclude();
    }
}