namespace Unicv.Streaming.Api.Models.Requests;

public class ProfileRequest
{
    public string Name { get; set; }

    public bool IsChildrenProfile { get; set; }

    public int AccountId { get; set; }
}
