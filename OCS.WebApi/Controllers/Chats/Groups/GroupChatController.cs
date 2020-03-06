using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.Services.Contracts.Chats.Group;
using OCS.WebApi.Extensions;
using System.Threading.Tasks;

namespace OCS.WebApi.Controllers.Chats.Groups
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class GroupChatController : Controller
    {
        private readonly IGroupChatService _groupChatService;

        public GroupChatController(IGroupChatService privateChatService)
        {
            _groupChatService = privateChatService;
        }

        /// <summary>
        /// Create group chat
        /// </summary>
        /// <param name="chatDto">
        /// <see cref="CreateGroupChatDto"/>
        /// </param>
        /// <returns>
        /// <see cref="GetGroupChatDto"/>
        /// </returns>  
        /// <response code="400">If data in the request body is wrong</response>
        [HttpPost]
        public async Task<IActionResult> CreateChatAsync([FromBody] CreateGroupChatDto chatDto)
        {
            chatDto.UserId = User.GetUserId();

            GetGroupChatDto chat = await _groupChatService.CreateChatAsync(chatDto);

            return Ok(chat);
        }

        /// <summary>
        /// Get user group chats
        /// </summary>
        /// <returns>
        /// <see cref="GetGroupChatDto"/>
        /// </returns>  
        [HttpGet]
        public async Task<IActionResult> GetUserChatsAsync()
        {
            var chats = await _groupChatService.GetUserGroupChatsAsync(User.GetUserId());
            return Ok(chats);
        }
    }
}