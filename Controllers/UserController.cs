using DTO;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ICommonService<User> _userService;
    private readonly IPaginationUtil _paginationUtil;
    public UserController(
        [FromKeyedServices("UserService")] ICommonService<User> userService,
        IPaginationUtil paginationUtil
    )
    {
        _userService = userService;
        _paginationUtil = paginationUtil;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int recordsPerPage = 10)
    {
        List<User> users = await _userService.GetAllAsync();

        if (users.Count == 0)
            return NotFound();

        var result = _paginationUtil.GetPagination(users, page, recordsPerPage);
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        User user = await _userService.GetByIdAsync(id);

        if (user.Id == 0)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDTO user)
    {
        User newUser = new User{
            Id = 0,
            Name = user.Name,
            Email = user.Email,
            Password = user.EncryptPassword(user.Password!),
            IsActived = user.IsActived
        };
        
        await _userService.CreateAsync(newUser);

        if (newUser.Id == 0)
            return NotFound();

        return Ok(newUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.Id)
            return BadRequest("Id is different");

        User result = await _userService.UpdateAsync(id, user);

        if (result.Id == 0)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(User user)
    {
        await _userService.DeleteAsync(user);
        return NoContent();
    }
}