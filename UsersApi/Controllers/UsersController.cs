using System;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;
using UsersApi.Providers;

namespace UsersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<User> Get()
    {
        _logger.LogInformation("Get All Users");
       //foreach (User user in _users)
       foreach (User user in _userRepository.GetUsers())
       {
            yield return user;
       }
    }

    [HttpGet("{id}")]
    public User Get(long id)
    {
        _logger.LogInformation($"Get User for ID {id}");
        return _userRepository.GetUser(id);
    }

    [HttpPost()]
    public void Post(User user)
    {
        _logger.LogInformation($"Adding a new user");
        _userRepository.AddUser(user);
    }

    [HttpPut("id")]
    public void Put(long id, User user)
    {
        _logger.LogInformation($"Updating user with id {id}");
        // TODO: Replace user model with contract that doesn't have the ID.
        user.ID = id;
        _userRepository.UpdateUser(user);
    }

    [HttpDelete("{id}")]
    public void Delete(long id)
    {
        _logger.LogInformation($"Deleting user with id {id}");
        _userRepository.DeleteUser(id);
    }
}
