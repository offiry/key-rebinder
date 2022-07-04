using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Persistance.Configurations
{
    internal class GameKeysConfiguration : IEntityTypeConfiguration<GameKeyEntity>
    {
        public void Configure(EntityTypeBuilder<GameKeyEntity> builder)
        {
            builder
                .HasKey(e => new { e.Id });
            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder
                .Property(e => e.KeyId_FK)
                .IsRequired();
            builder
               .Property(e => e.KeyModifierId_FK)
               .IsRequired();
            builder
               .Property(e => e.WindowGameNameId_FK)
               .IsRequired();
            builder
               .Property(e => e.TargetKey_FK)
               .IsRequired();
            builder
                .HasOne(e => e.GameEntity)
                .WithMany(p => p.GameKeysEntity)
                .HasForeignKey(e => new { e.WindowGameNameId_FK });
            builder
                .HasOne(e => e.KeyEntity)
                .WithMany(p => p.GameKeysEntity)
                .HasForeignKey(e => new { e.KeyId_FK });
            builder
                .HasOne(e => e.KeyModifierEntity)
                .WithMany(p => p.GameKeysEntity)
                .HasForeignKey(e => new { e.KeyModifierId_FK });
            builder
                .HasOne(e => e.TargetKeyEntity)
                .WithMany(p => p.GameTargetKeysEntity)
                .HasForeignKey(e => new { e.TargetKey_FK });
        }
    }
}
