using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UM.Domain;

namespace Common.Persistence.EntityTypeConfigurations;

public class ApplicationUserRoleConfiguration: IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasKey(u => u.ApplicationUserRoleId);
        builder.Property(u => u.Name).IsUnicode().HasMaxLength(20);
    }
}