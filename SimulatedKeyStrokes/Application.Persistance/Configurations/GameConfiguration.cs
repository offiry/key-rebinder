using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Persistance.Configurations
{
    internal class GameConfiguration : IEntityTypeConfiguration<GameEntity>
    {
        public void Configure(EntityTypeBuilder<GameEntity> builder)
        {
            builder
                .HasKey(e => new { e.Id });
            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder
                .Property(e => e.WindowGameName)
                .IsRequired()
                .HasMaxLength(450);
            builder
                .Property(e => e.DisplayUIGameName)
                .IsRequired()
                .HasMaxLength(450);
        }
    }
}
