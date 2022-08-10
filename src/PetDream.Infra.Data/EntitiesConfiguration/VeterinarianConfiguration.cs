using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDream.Domain.Entities;

namespace PetDream.Infra.Data.EntitiesConfiguration
{
    public class VeterinarianConfiguration : IEntityTypeConfiguration<Veterinarian>
    {
        public void Configure(EntityTypeBuilder<Veterinarian> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Registration).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Phone).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Status).IsRequired();

            builder.HasData(
                new Veterinarian(1, "Marcia Bezerra", "31234", "marciabezerra@gmail.com", "Marcia@123456", "81994584453"),
                new Veterinarian(2, "Mayara Costa", "44451", "mayaracosta@gmail.com", "Mayara@123456", "11994457777")
            );
        }
    }
}