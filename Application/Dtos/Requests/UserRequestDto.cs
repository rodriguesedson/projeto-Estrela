using System.ComponentModel.DataAnnotations;

namespace api.Models.Requests;

public class UserRequestDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public DateOnly BirthDate { get; set; }
}