using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Persistance.Configurations
{
    internal class KeysConfiguration : IEntityTypeConfiguration<KeyEntity>
    {
        public void Configure(EntityTypeBuilder<KeyEntity> builder)
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
        }
    }
}
