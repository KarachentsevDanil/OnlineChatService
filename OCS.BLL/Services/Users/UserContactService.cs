using AutoMapper;
using Microsoft.Extensions.Logging;
using OCS.BLL.DTOs.Users;
using OCS.BLL.Services.Contracts.Users;
using OCS.DAL.Entities.Users;
using OCS.DAL.UnitOfWorks.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Users
{
    public class UserContactService : IUserContactService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<UserContactService> _logger;

        public UserContactService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<UserContactService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<GetUserContactDto> AddUserToContactAsync(AddUserToContactDto addUserToContactDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Add user contact {@UserContact}", addUserToContactDto);

            UserContact userContact = _mapper.Map<UserContact>(addUserToContactDto);

            DateTime currentDate = DateTime.UtcNow;

            userContact.CreatedAt = userContact.UpdatedAt = currentDate;

            _unitOfWork.UserContactRepository.Create(userContact);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetUserContactDto>(userContact);
        }

        public async Task<IImmutableList<GetUserContactDto>> GetUserContactsAsync(string userId, CancellationToken ct = default)
        {
            _logger.LogInformation("Get user contact by userId {UserId}", userId);

            ICollection<UserContact> userContacts =
                await _unitOfWork.UserContactRepository.GetUserContactsAsync(userId, ct);

            return _mapper.Map<ICollection<GetUserContactDto>>(userContacts).ToImmutableList();
        }
    }
}