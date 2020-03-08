using Microsoft.AspNetCore.Mvc;
using OCS.BLL.DTOs.Users;
using OCS.BLL.Exceptions.Users;
using OCS.BLL.Services.Contracts.Users;
using OCS.WebApi.Extensions;
using System.Threading.Tasks;

namespace OCS.WebApi.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserAuthorizationService _authorizationService;

        private readonly IUserService _userService;

        public UserController(IUserAuthorizationService authorizationService, IUserService userService)
        {
            _authorizationService = authorizationService;
            _userService = userService;
        }

        /// <summary>
        /// Login user with email and password
        /// </summary>
        /// <param name="loginDto">
        /// <see cref="UserLoginDto"/>
        /// </param>
        /// <returns>
        /// <see cref="UserTokenDto "/>
        /// </returns>  
        /// <response code="401">If credentials are wrong</response> 
        /// <response code="400">If data in the request body is wrong</response> 
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(UserTokenDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var result = await _authorizationService.LoginUserAsync(loginDto);

                if (result == null)
                {
                    return Unauthorized();
                }

                SetUserOnlineDto onlineStatus = new SetUserOnlineDto
                {
                    IsOnline = true,
                    UserId = result.User.Id
                };

                await _userService.SetUserOnlineStatusAsync(onlineStatus);

                return Ok(result);
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (UserLoginFailedException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userDto">
        /// <see cref="UserRegistrationDto"/>
        /// </param>
        /// <returns>
        /// <see cref="GetUserDto "/>
        /// </returns>  
        /// <response code="400">If data in the request body is wrong</response> 
        /// <response code="400">If user with this email already exists</response> 
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(UserTokenDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationDto userDto)
        {
            try
            {
                var result = await _authorizationService.RegisterUserAsync(userDto);

                if (result == null)
                {
                    return BadRequest("User already exists");
                }

                return Json(result);
            }
            catch (UserAlreadyExistsException)
            {
                return BadRequest();
            }
            catch (UserRegistrationFailedException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            SetUserOnlineDto onlineStatus = new SetUserOnlineDto
            {
                IsOnline = false,
                UserId = User.GetUserId()
            };

            await _userService.SetUserOnlineStatusAsync(onlineStatus);

            return NoContent();
        }
    }
}