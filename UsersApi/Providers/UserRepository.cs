using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;

namespace UsersApi.Providers;

/// <summary>
/// In memory user repository.  Note that this implementation isn't thread safe.
/// </summary>
public class InMemoryUserRepository : IUserRepository
{
    public static long nextId = 3;
    public static readonly Dictionary<long, User> _repository = new Dictionary<long, User>()
    {
        {0,  new User() {
            ID = 0,
            FirstName = "Bob",
            LastName = "Smith",
            Email = "bob.smith@nowhere.local",
            DateOfBirth = new DateTime(1954, 11,24),
            PhoneNumber = 1234567890         
        }},
        {1, new User() {
            ID = 1,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@nowhere.local",
            DateOfBirth = new DateTime(1957, 8,22),
            PhoneNumber = 1234567890          
        }},
        {2, new User() {
            ID = 2,
            FirstName = "Fred",
            LastName = "Smith",
            Email = "fred.smith@nowhere.local",
            DateOfBirth = new DateTime(1980, 7,3),
            PhoneNumber = 1234567890
        }}
    };

    public long AddUser(User user)
    {
        ValidateEmailIsUnique(user.Email);
        user.ID = nextId++;
        _repository.Add(user.ID, user);
        return user.ID;
    }

    /// <summary>
    /// Deletes user for specified id or nothing if the user doesn't exist.
    /// </summary>
    /// <param name="id"></param>
    public void DeleteUser(long id) => _repository.Remove(id);

    /// <summary>
    /// Returns a specific user
    /// </summary>
    /// <param name="id">id of the user to return</param>
    /// <returns>User matching the id passed in.</returns>
    public User GetUser(long id) => _repository[id];

    public IEnumerable<User> GetUsers()
    {
        foreach (User user in _repository.Values)
        {
            yield return user;
        }
    }

    public void UpdateUser(User user) 
    {
        ValidateEmailIsUnique(user.Email);
        _repository[user.ID] = user;
    }

    private void ValidateEmailIsUnique(string? email)
    {
        // TODO: expand validation to be valid email.
        if (String.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("User must have an email address");
        }
        if (_repository.Any(u => u.Value.Email?.Equals(email) ?? false))
        {
            throw new ArgumentException("User already exists with the specified Email");
        }
    }
}