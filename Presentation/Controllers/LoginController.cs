using DTOs.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Abstracts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Entities.EmailEntity;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public LoginController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("/register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserDTO userDTO)
        {
            try
            {
                _userService.Create(userDTO);

                var msg = new Message(
                    new string[] { userDTO.Email }, "Registration", "" +
                    $"<p>Dear {userDTO.Username}! <br> " +
                    $"You registered successfully!</p>"
                    );

                _emailService.SendEmail(msg);
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpPost]
        [Route("/login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserDTO userDTO)
        {
            try
            {
                var result = _userService.Login(userDTO);
                Authenticate(result);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        [HttpPost]
        [Route("/logout")]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync();
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = exception.Message });
            }
        }

        private void Authenticate(UserDTO userDTO)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", userDTO.Id.ToString()),
                new Claim("Username", userDTO.Username),
                new Claim("Email", userDTO.Email),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }
    }
}