using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.Exceptions.Chats;
using OCS.BLL.Exceptions.Users;
using OCS.BLL.Services.Contracts.Chats.Private;
using OCS.WebApi.Extensions;
using System.Threading.Tasks;
using OCS.BLL.DTOs.Chats.Queries;

namespace OCS.WebApi.Controllers.Chats.Privates
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PrivateChatController : Controller
    {
        private readonly IPrivateChatService _privateChatService;

        private readonly IPrivateChatMessageService _privateChatMessageService;

        public PrivateChatController(IPrivateChatService privateChatService, IPrivateChatMessageService privateChatMessageService)
        {
            _privateChatService = privateChatService;
            _privateChatMessageService = privateChatMessageService;
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
            try
            {
                privateChatDto.UserId = User.GetUserId();

                GetPrivateChatDto chat = await _privateChatService.CreatePrivateChatAsync(privateChatDto);

                return Ok(chat);
            }
            catch (UserNotFoundException)
            {
                return BadRequest();
            }
            catch (PrivateChatAlreadyExistException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get user private chats
        /// </summary>
        /// <returns>
        /// <see cref="GetPrivateChatDto"/>
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetPrivateChatViewDto), 200)]
        public async Task<IActionResult> GetUserChatsAsync([FromQuery] GetPrivateChatsQueryDto query)
        {
            query.UserId = User.GetUserId();

            var chats = await _privateChatService.GetUserPrivateChatsViewAsync(query);

            return Ok(chats);
        }

        /// <summary>
        /// Get private chat messages
        /// </summary>
        /// <returns>
        /// <see cref="GetPrivateChatMessageDto"/>
        /// </returns>
        [HttpGet("messages")]
        [ProducesResponseType(typeof(GetPrivateChatMessageDto), 200)]
        public async Task<IActionResult> GetUserChatsAsync([FromQuery] GetPagedMessagesQueryDto query)
        {
            var messages = await _privateChatMessageService.GetMessagesAsync(query);
            return Ok(messages);
        }
    }
}