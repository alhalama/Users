using FrameworkExtensions;
using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models;
public class User
{
    public Guid ID {get; set;}
    [Required(ErrorMessage = "FirstName is required")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }    
    public DateTime DateOfBirth { get; set; }
    public long PhoneNumber { get; set; }

    public int Age => DateOfBirth.YearsBeforeToday();
}