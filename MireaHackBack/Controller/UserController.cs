using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using MireaHackBack.Model.User;
using MireaHackBack.Response.User;
using MireaHackBack.Services;

namespace MireaHackBack.Controller;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Запросить регистрацию по данному Email
    /// </summary>
    /// <response code="200">Код отправлен на почту.</response>
    /// <response code="409">Аккаунт с данной электронной почтой существует.</response>
    /// <response code="429">Письмо уже было запрошено менее минуты назад.</response>
    [Route("register")]
    [HttpPost]
    public IActionResult Register([FromQuery] UserRegistrationModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(model);
        }
        
        var result = _userService.Register(model);
        return StatusCode(result.StatusCode, result.Payload);
    }   

    /// <summary>
    /// Завершить регистрацию аккаунта
    /// </summary>
    /// <response code="200">Аккаунт зарегистрирован.</response>
    /// <response code="401">Некорректная электронная почта или код регистрации, либо действие кода регистрации истекло.</response>
    [Route("finishRegistration")]
    [HttpPost]
    public IActionResult Register([FromQuery] UserFinishRegistrationModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(model);
        }
        
        var result = _userService.FinishRegistration(model);
        return StatusCode(result.StatusCode, result.Payload);
    }

    /// <summary>
    /// Войти в аккаунт
    /// </summary>
    /// <response code="200">Вход успешен.</response>
    /// <response code="401">Неверное имя пользователя или пароль.</response>
    [ProducesResponseType(typeof(TokenResponse), (int)HttpStatusCode.OK), ]
    [Route("login")]
    [HttpPost]
    public IActionResult Login([FromQuery] UserLoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(model);
        }
        
        var result = _userService.Login(model);
        return StatusCode(result.StatusCode, result.Payload);
    }
}