using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDream.Domain.Entities;

namespace PetDream.Infra.Data.EntitiesConfiguration
{
    public class VeterinaryCareRecordConfiguration : IEntityTypeConfiguration<VeterinaryCareRecord>
    {
        public void Configure(EntityTypeBuilder<VeterinaryCareRecord> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.ServiceDate).IsRequired();
            builder.Property(p => p.PetWeight).HasPrecision(3,2).IsRequired();
            builder.Property(p => p.PetObservations).HasMaxLength(1024).IsRequired();
            builder.Property(p => p.Diagnosis).HasMaxLength(1024).IsRequired();
        }
    }
}