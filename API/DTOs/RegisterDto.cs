using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{

    [Required(ErrorMessage = "Please enter a username")]
    [MinLength(3, ErrorMessage = "Please enter a username at least 3 characters")]
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
