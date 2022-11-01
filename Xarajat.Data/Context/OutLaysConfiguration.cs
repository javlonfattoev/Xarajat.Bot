using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xarajat.Data.Entities;

namespace Xarajat.Data.Context;

public class OutLaysConfiguration
{
    public static void Configure(EntityTypeBuilder<Outlay> builder)
    {
        builder.ToTable("outlays")
            .HasKey(outlay => outlay.Id);

        builder.Property(outlay => outlay.Description)
            .HasColumnType("nvarchar(255)")
            .IsRequired(false);

        builder.Property(outlay => outlay.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(outlay => outlay.RoomId)
            .HasColumnName("room_id")
            .IsRequired();

        builder.Ignore(outlay => outlay.ToReadable);

        builder.HasOne(outlay => outlay.User)
            .WithMany(user => user.Outlays)
            .HasForeignKey(outlay => outlay.UserId);

        builder.HasOne(outlay => outlay.Room)
            .WithMany(room => room.Outlays)
            .HasForeignKey(outlay => outlay.RoomId);
    }
}