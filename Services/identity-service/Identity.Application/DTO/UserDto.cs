namespace Identity.Applications.DTO;

public class UserDto
{
    public Guid UserId { get; init; }
    public string Email { get; init; }
    public List<string> Role { get; init; } = new();
    public bool IsActive { get; init; }
    public DateTime CreateAt { get; init; }
}