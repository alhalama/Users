using Microsoft.AspNetCore.Identity;
using MySqlConnector;
using System.Data;
using UsersApi.Models;

namespace UsersApi.UsersProvider;

public class SqlUserRepository : IUserRepository
{
    private const string AddUserSql = "INSERT INTO users (id, FirstName, LastName, Email, DateOfBirth, PhoneNumber) VALUES (@id, @firstName, @lastName, @email, @dateOfBirth, @phoneNumber);Select LAST_INSERT_ID();";
    private const string DeleteUserSql = "DELETE FROM users WHERE id = @id";    
    private const string GetSpecificUserSql = $"{GetAllUsersSql} WHERE ID = @id";
    private const string GetAllUsersSql = "SELECT id, FirstName, LastName, Email, DateOfBirth, PhoneNumber FROM users";
    private const string UpdateUserSql = "UPDATE users SET FirstName = @firstName, LastName = @lastName, Email = @email, DateOfBirth = @dateOfBirth, PhoneNumber = @phoneNumber WHERE id = @id";
    private IConfiguration Configuration { get; }
    private string UserConnectionString => Configuration["ConnectionStrings:UsersConnection"] ?? throw new InvalidOperationException("UsersConnection ConnectionString must be set in configuration.");
    public SqlUserRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void AddUser(User user)
    {
        using MySqlConnection connection = new MySqlConnection(UserConnectionString);
        using MySqlCommand command = new MySqlCommand(AddUserSql, connection);
        command.Parameters.AddWithValue("@id", user.ID);
        command.Parameters.AddWithValue("@firstName", user.FirstName);
        command.Parameters.AddWithValue("@lastName", user.LastName);
        command.Parameters.AddWithValue("@email", user.Email);
        command.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);
        command.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }
    }

    public void DeleteUser(Guid id)
    {
        using MySqlConnection connection = new MySqlConnection(UserConnectionString);
        using MySqlCommand command = new MySqlCommand(DeleteUserSql, connection);
        command.Parameters.AddWithValue("@id", id);
        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }
    }

    public User GetUser(Guid id)
    {
        using MySqlConnection connection = new MySqlConnection(UserConnectionString);
        using MySqlCommand command = new MySqlCommand(GetSpecificUserSql, connection);
        command.Parameters.AddWithValue("@id", id);
        try
        {
            connection.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User()
                {
                    ID = reader.GetGuid("id"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Email = reader.GetString("Email"),
                    DateOfBirth = reader.GetDateTime("DateOfBirth").Date,
                    PhoneNumber = reader.GetInt64("PhoneNumber")
                };
            }
            reader.Close();
        }
        finally
        {            
            connection.Close();
        }
        throw new KeyNotFoundException($"Unable to find user for id: {id}");
    }

    public IEnumerable<User> GetUsers()
    {
                using MySqlConnection connection = new MySqlConnection(UserConnectionString);
        using MySqlCommand command = new MySqlCommand(GetAllUsersSql, connection);
        try
        {
            connection.Open();
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new User()
                {
                    ID = reader.GetGuid("id"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Email = reader.GetString("Email"),
                    DateOfBirth = reader.GetDateTime("DateOfBirth").Date,
                    PhoneNumber = reader.GetInt64("PhoneNumber")
                };
            }
            reader.Close();
        }
        finally
        {
            connection.Close();
        }
    }

    public void UpdateUser(User user)
    {
        using MySqlConnection connection = new MySqlConnection(UserConnectionString);
        using MySqlCommand command = new MySqlCommand(UpdateUserSql, connection);
        command.Parameters.AddWithValue("@id", user.ID);
        command.Parameters.AddWithValue("@firstName", user.FirstName);
        command.Parameters.AddWithValue("@lastName", user.LastName);
        command.Parameters.AddWithValue("@email", user.Email);
        command.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);
        command.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }
    }
}