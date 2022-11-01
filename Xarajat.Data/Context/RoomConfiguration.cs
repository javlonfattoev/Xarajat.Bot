using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xarajat.Data.Entities;

namespace Xarajat.Data.Context;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("rooms")
            .Property(room => room.Name)
            .IsRequired()
        .HasColumnType("nvarchar(50)");

        builder.Property(room => room.Key)
            .HasColumnType("nvarchar(32)")
        .IsRequired();

        builder.Property(room => room.Status)
            .HasDefaultValue(RoomStatus.Created)
            .IsRequired();
    }
}