using EstatePredict.DTOs;
using EstatePredict.Requests;
using EstatePredict.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstatePredict.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET api/user/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDTO>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound(new { message = $"User with id {id} was not found." });

        return Ok(user);
    }

    // POST api/user/register
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterUserRequest request)
    {
        var createdUser = await _userService.RegisterAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    // POST api/user/login
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
    public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequest request)
    {
        var result = await _userService.LoginAsync(request);
        return Ok(result);
    }
}