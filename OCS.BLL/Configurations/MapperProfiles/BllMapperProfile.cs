using AutoMapper;
using OCS.BLL.DTOs.Chats.Group;
using OCS.BLL.DTOs.Chats.Private;
using OCS.BLL.DTOs.Chats.Queries;
using OCS.BLL.DTOs.Users;
using OCS.DAL.Entities.Chats;
using OCS.DAL.Entities.Chats.Queries;
using OCS.DAL.Entities.Users;
using OCS.DAL.Entities.Views.Chats;
using OCS.DAL.Queries.Chats;
using OCS.DAL.Queries.Users;

namespace OCS.BLL.Configurations.MapperProfiles
{
    public class BllMapperProfile : Profile
    {
        public BllMapperProfile()
        {
            CreateMap<User, GetUserDto>();

            CreateMap<UserRegistrationDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(p => p.Email));

            CreateMap<AddUserToContactDto, UserContact>();

            CreateMap<UserContact, GetUserContactDto>();

            CreateMap<PrivateChat, GetPrivateChatDto>();

            CreateMap<PrivateChatMessage, GetPrivateChatMessageDto>();

            CreateMap<PrivateChatView, GetPrivateChatViewDto>();

            CreateMap<GetPrivateChatsQueryDto, GetPrivateChatsQuery>();

            CreateMap<GetUsersQueryDto, GetUsersQuery>();

            CreateMap<GroupChat, GetGroupChatDto>();

            CreateMap<GroupChatMessage, GetGroupChatMessageDto>();

            CreateMap<GetPagedMessagesQueryDto, GetPagedMessagesQuery>();

            CreateMap<AddUserToGroupChatDto, UserGroupChat>()
                .ForMember(dest => dest.GroupChatId, opt => opt.MapFrom(p => p.ChatId));

            CreateMap<CreateGroupChatDto, GroupChat>()
                .ForMember(t=> t.OwnerId, opt => opt.MapFrom(p => p.UserId));

            CreateMap<CreateGroupChatMessageDto, GroupChatMessage>()
                .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(p => p.UserId));

            CreateMap<CreatePrivateChatDto, PrivateChat>()
                .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(p => p.UserId))
                .ForMember(dest => dest.InvitedUserId, opt => opt.MapFrom(p => p.ContactId));

            CreateMap<CreatePrivateChatMessageDto, PrivateChatMessage>()
                .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(p => p.UserId));
        }
    }
}