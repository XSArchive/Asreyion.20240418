namespace Asreyion.Core.Areas.User.Models;

public class ProfileViewModel
{
    public required string Id { get; set; }
    public required string Username { get; set; }
    public required List<string> Roles { get; set; }
}