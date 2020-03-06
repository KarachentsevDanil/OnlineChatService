using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OCS.BLL.Configurations;
using OCS.BLL.DTOs.Users;
using OCS.BLL.Extensions;
using OCS.BLL.Services.Contracts.Users;
using OCS.DAL.Entities.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.BLL.Services.Users
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly AuthenticationConfiguration _configuration;

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly ILogger<UserAuthorizationService> _logger;

        public UserAuthorizationService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            AuthenticationConfiguration configuration,
            IUserService userService,
            IMapper mapper,
            ILogger<UserAuthorizationService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserTokenDto> LoginUserAsync(UserLoginDto loginDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Login as user = {@User}", loginDto);

            SignInResult result = await _signInManager
                .PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                GetUserDto user = await _userService.GetByEmailAsync(loginDto.Email, ct);

                string token = GenerateToken(user);

                UserTokenDto tokenResponse = new UserTokenDto
                {
                    User = user,
                    AccessToken = token,
                    ExpiresIn = (int)TimeSpan.FromDays(_configuration.TokenExpirationPeriodInDay).TotalSeconds
                };

                return tokenResponse;
            }

            return null;
        }

        public async Task<GetUserDto> RegisterUserAsync(UserRegistrationDto registrationDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Register new user = {@User}", registrationDto);

            User user = _mapper.Map<User>(registrationDto);

            IdentityResult result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
            {
                return _mapper.Map<GetUserDto>(user);
            }

            return null;
        }

        private string GenerateToken(GetUserDto user)
        {
            Claim[] claims = new[]
            {
                new Claim(nameof(User.Id), user.Id),
                new Claim(nameof(User.Email), user.Email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,
                    new DateTimeOffset(DateTime.Now.AddDays(_configuration.TokenExpirationPeriodInDay)).ToUnixTimeSeconds().ToString()),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(_configuration.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
