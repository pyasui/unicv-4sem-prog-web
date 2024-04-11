namespace Unicv.Streaming.Api.Data.Entities;

public class Profile
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsChildrenProfile { get; set; }

    public int AccountId { get; set; }

    public DateTime CreatedAt { get; set; }
}
