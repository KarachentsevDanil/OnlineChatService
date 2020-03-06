using Microsoft.AspNetCore.Mvc;
using OCS.BLL.DTOs.Users;
using OCS.BLL.Services.Contracts.Users;
using System.Threading.Tasks;

namespace OCS.WebApi.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserAuthorizationService _authorizationService;

        public UserController(IUserAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
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
            var result = await _authorizationService.LoginUserAsync(loginDto);

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
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
            var result = await _authorizationService.RegisterUserAsync(userDto);

            if (result == null)
            {
                return BadRequest("User already exists");
            }

            return Json(result);
        }
    }
}