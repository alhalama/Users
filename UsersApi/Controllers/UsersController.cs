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

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
        // TODO: dependency injection should be user here.
        _userRepository = new InMemoryUserRepository();
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<User> Get()
    {
       //foreach (User user in _users)
       foreach (User user in _userRepository.GetUsers())
       {
            yield return user;
       }
    }

    [HttpGet("{id}")]
    public User Get(long id)
    {
        return _userRepository.GetUser(id);
    }

    [HttpPost()]
    public void Post(User user)
    {
        _userRepository.AddUser(user);
    }

    [HttpPut("id")]
    public void Put(long id, User user)
    {
        // TODO: Replace user model with contract that doesn't have the ID.
        user.ID = id;
        _userRepository.UpdateUser(user);
    }

    [HttpDelete("{id}")]
    public void Delete(long id)
    {
        _userRepository.DeleteUser(id);
    }
}
