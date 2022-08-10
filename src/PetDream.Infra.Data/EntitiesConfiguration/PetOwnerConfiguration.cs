using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDream.Domain.Entities;

namespace PetDream.Infra.Data.EntitiesConfiguration
{
    public class PetOwnerConfiguration : IEntityTypeConfiguration<PetOwner>
    {
        public void Configure(EntityTypeBuilder<PetOwner> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Cpf).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Phone).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Status).IsRequired();

            builder.HasData(
                new PetOwner(1, "Fernanda Pessoa", "63997600031", "fernandapessoa@gft.com", "Gft@123456", "11994554345"),
                new PetOwner(2, "Carlos Diego", "91014153018", "carlosdiego@gft.com", "Gft@123456", "81994583345")
            );
        }
    }
}