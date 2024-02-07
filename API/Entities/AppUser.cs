using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;
#nullable disable

public class AppUser : IdentityUser<int> //<int> เพื่อใช้ int เป็น id, default คือ string
{

    // public byte[]? PasswordHash { get; internal set; }
    // public byte[]? PasswordSalt { get; internal set; }
    // public string? UserName { get; set; }
    // public int Id { get; set; }

    public DateOnly BirthDate { get; set; }
    public string Aka { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<Photo> Photos { get; set; } = new();
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string Gender { get; internal set; }
    public List<UserLike> LikedByUsers { get; set; }
    public List<UserLike> LikedUsers { get; set; }

    public List<Message> MessagesSent { get; set; }
    public List<Message> MessagesReceived { get; set; }




    public ICollection<AppUserRole> UserRoles { get; set; }
}

