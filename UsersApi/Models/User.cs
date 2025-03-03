using FrameworkExtensions;

namespace UsersApi.Models;
public class User
{
    public long ID {get; set;}
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public long PhoneNumber { get; set; }

    public int Age => DateOfBirth.YearsBeforeToday();
}