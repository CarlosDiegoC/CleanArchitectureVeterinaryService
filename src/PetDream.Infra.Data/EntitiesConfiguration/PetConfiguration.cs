using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDream.Domain.Entities;

namespace PetDream.Infra.Data.EntitiesConfiguration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Breed).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Color).HasMaxLength(50).IsRequired();
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(e => e.PetOwner).WithMany(e => e.Pets).HasForeignKey(e => e.PetOwnerId);
        }
    }
}