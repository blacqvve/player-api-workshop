using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(x=>x.PlayerId);
            builder.Property(p=>p.CreateDate).HasColumnType("TIMESTAMP");
            builder.Property(p=>p.LastLogin).HasColumnType("TIMESTAMP");
        }
    }
}