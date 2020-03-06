﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using OCS.BLL.DTOs.Users;
using OCS.BLL.Services.Contracts.Users;
using OCS.DAL.Entities.Users;
using OCS.DAL.UnitOfWorks.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<UserService> _logger;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<GetUserDto> GetByIdAsync(string id, CancellationToken ct)
        {
            _logger.LogInformation("Get user by id {UserId}", id);

            User user = await _unitOfWork.UserRepository.GetAsync(id, ct);

            if (user == null)
            {
                _logger.LogInformation("User with id {UserId} not found", id);
                return null;
            }

            return _mapper.Map<GetUserDto>(user);
        }

        public async Task<GetUserDto> GetByEmailAsync(string email, CancellationToken ct)
        {
            _logger.LogInformation("Get user by email {Email}", email);

            User user = await _unitOfWork.UserRepository.GetByEmailAsync(email, ct);

            if (user == null)
            {
                _logger.LogInformation("User with email {Email} not found", email);
                return null;
            }

            return _mapper.Map<GetUserDto>(user);
        }
    }
}