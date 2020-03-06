using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.Services.Contracts.Chats.Private;
using OCS.WebApi.Extensions;
using System.Threading.Tasks;

namespace OCS.WebApi.Controllers.Chats.Privates
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PrivateChatController : Controller
    {
        private readonly IPrivateChatService _privateChatService;

        public PrivateChatController(IPrivateChatService privateChatService)
        {
            _privateChatService = privateChatService;
        }

        /// <summary>
        /// Create private chat
        /// </summary>
        /// <param name="privateChatDto">
        /// <see cref="CreatePrivateChatDto"/>
        /// </param>
        /// <returns>
        /// <see cref="GetPrivateChatDto"/>
        /// </returns>  
        /// <response code="400">If data in the request body is wrong</response>
        [HttpPost]
        [ProducesResponseType(typeof(GetPrivateChatDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateChatAsync([FromBody] CreatePrivateChatDto privateChatDto)
        {
            privateChatDto.UserId = User.GetUserId();

            GetPrivateChatDto chat = await _privateChatService.CreatePrivateChatAsync(privateChatDto);

            return Ok(chat);
        }

        /// <summary>
        /// Get user private chats
        /// </summary>
        /// <returns>
        /// <see cref="GetPrivateChatDto"/>
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetPrivateChatDto), 200)]
        public async Task<IActionResult> GetUserChatsAsync()
        {
            var chats = await _privateChatService.GetUserPrivateChatsAsync(User.GetUserId());
            return Ok(chats);
        }
    }
}