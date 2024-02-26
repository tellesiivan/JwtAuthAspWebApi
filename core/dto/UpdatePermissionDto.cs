using System.ComponentModel.DataAnnotations;

namespace JwtAuthAspWebApi.core.dto;

public class UpdatePermissionDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
}