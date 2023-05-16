namespace Server.Models;

public interface IAdmin
{
    string? Name { get; set; }
    string? Email { get; set; }
    string? Password { get; set; }
}
