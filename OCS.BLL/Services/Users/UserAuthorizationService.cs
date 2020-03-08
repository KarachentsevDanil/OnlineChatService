using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OCS.BLL.Configurations;
using OCS.BLL.DTOs.Users;
using OCS.BLL.Exceptions.Users;
using OCS.BLL.Extensions;
using OCS.BLL.Services.Contracts.Users;
using OCS.DAL.Entities.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

            GetUserDto user = await _userService.GetByEmailAsync(loginDto.Email, ct);

            if (user == null)
            {
                _logger.LogInformation("User with email {Email} not found", loginDto.Email);
                throw new UserNotFoundException();
            }

            SignInResult result = await _signInManager
                .PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return GenerateToken(user);
            }

            throw new UserLoginFailedException();
        }

        public async Task<GetUserDto> RegisterUserAsync(UserRegistrationDto registrationDto, CancellationToken ct = default)
        {
            _logger.LogInformation("Register new user = {@User}", registrationDto);

            User user = _mapper.Map<User>(registrationDto);
            
            GetUserDto dbUser = await _userService.GetByEmailAsync(registrationDto.Email, ct);

            if (dbUser != null)
            {
                _logger.LogInformation("User with email {Email} already exists", registrationDto.Email);
                throw new UserAlreadyExistsException();
            }

            IdentityResult result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
            {
                return _mapper.Map<GetUserDto>(user);
            }

            throw new UserRegistrationFailedException(string.Join(" ", result.Errors.Select(t => t.Description)));
        }

        private UserTokenDto GenerateToken(GetUserDto user)
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

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            UserTokenDto tokenResponse = new UserTokenDto
            {
                User = user,
                AccessToken = tokenValue,
                ExpiresIn = (int)TimeSpan.FromDays(_configuration.TokenExpirationPeriodInDay).TotalSeconds
            };

            return tokenResponse;
        }
    }
}
