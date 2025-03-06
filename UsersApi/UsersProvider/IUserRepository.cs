using UsersApi.Models;

namespace UsersApi.UsersProvider;
public interface IUserRepository
{
    /// <summary>
    /// Adds a new user
    /// </summary>
    /// <param name="user">user to add.  Note that the ID will be ignored and the new Id will be returned</param>
    /// <returns>Id of the user that was added.</returns>
    void AddUser(User user);

    /// <summary>
    /// Deletes specified user if exists
    /// </summary>
    /// <param name="id">Id of the user to delete.</param>
    void DeleteUser(Guid id);

    /// <summary>
    /// Returns a enumerable of all users
    /// </summary>
    /// <returns>IEnumerable<User></returns>
    IEnumerable<User> GetUsers();

    /// <summary>
    /// Get a specific user
    /// </summary>
    /// <param name="id">Id of the user to return.</param>
    /// <returns>User</returns>
    User GetUser(Guid id);

    /// <summary>
    /// Updates a user.
    /// </summary>
    /// <param name="user">Updated User.</param>
    void UpdateUser(User user);
}