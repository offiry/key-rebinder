using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Persistance.Configurations
{
    internal class KeyModifiersConfiguration : IEntityTypeConfiguration<KeyModifierEntity>
    {
        public void Configure(EntityTypeBuilder<KeyModifierEntity> builder)
        {
            builder
                .HasKey(e => new { e.Id });
            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder
                .Property(e => e.Key)
                .IsRequired()
                .HasMaxLength(450);
            builder
                .Property(e => e.KeyModifier)
                .IsRequired()
                .HasMaxLength(450);
        }
    }
}
