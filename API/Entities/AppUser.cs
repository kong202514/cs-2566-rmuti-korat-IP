namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public string? UserName { get; set; }

    //snippet "prop" then tap
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public required byte[] test { get; set; }
    public required byte[] test2 { get; set; }
    public required byte[] test3 { get; set; }
    public required byte[] test4 { get; set; }
}