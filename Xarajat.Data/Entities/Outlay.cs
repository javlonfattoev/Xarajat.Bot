namespace Xarajat.Data.Entities;

public class Outlay
{
    public int Id { get; set; }

    public string? Description { get; set; }
    public int Cost { get; set; }

    public int? UserId { get; set; }
    public User? User { get; set; }

    public int? RoomId { get; set; }
    public Room? Room { get; set; }

    //[NotMapped]
    public string ToReadable => $"{Cost}, {Description}";
}