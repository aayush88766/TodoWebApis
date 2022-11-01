using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TodoListWebAPIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpGet]
    public IActionResult Testing()
    {
        return Ok("Working");
    }

    [HttpPost]
    public IActionResult Login(LoginRequest request)
    {
        return Ok(request);
    }
}

public class LoginRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    [PasswordEquality]
    public string? ConfirmPassword { get; set; }
}

public class PasswordEquality : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var request = validationContext.ObjectInstance as LoginRequest;
        if (request?.Password != request?.ConfirmPassword)
        {
            return new ValidationResult("Password does not match");
        }

        return ValidationResult.Success;
    }
}