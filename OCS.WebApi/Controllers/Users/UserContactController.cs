using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OCS.BLL.DTOs.Users;
using OCS.BLL.Services.Contracts.Users;
using OCS.WebApi.Extensions;
using System.Threading.Tasks;

namespace OCS.WebApi.Controllers.Users
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UserContactController : Controller
    {
        private readonly IUserContactService _userContactService;

        public UserContactController(IUserContactService userContactService)
        {
            _userContactService = userContactService;
        }

        /// <summary>
        /// Add user to contact
        /// </summary>
        /// <param name="userContact">
        /// <see cref="AddUserToContactDto"/>
        /// </param>
        /// <returns>
        /// <see cref="GetUserContactDto"/>
        /// </returns>  
        /// <response code="400">If data in the request body is wrong</response>
        [HttpPost]
        [ProducesResponseType(typeof(GetUserContactDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddNewContactAsync([FromBody] AddUserToContactDto userContact)
        {
            userContact.UserId = User.GetUserId();

            GetUserContactDto result = await _userContactService.AddUserToContactAsync(userContact);

            return Ok(result);
        }

        /// <summary>
        /// Get all user contacts
        /// </summary>
        /// <returns>
        /// <see cref="GetUserContactDto"/>
        /// </returns>  
        [HttpGet]
        [ProducesResponseType(typeof(GetUserContactDto), 200)]
        public async Task<IActionResult> GetUserContactsAsync()
        {
            var contacts = await _userContactService.GetUserContactsAsync(User.GetUserId());
            return Ok(contacts);
        }
    }
}