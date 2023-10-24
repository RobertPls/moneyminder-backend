using Application.UseCase.Command.Security.Login;
using Application.Utils;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Command.Security.Login
{
    internal class LoginHandler : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly JwtOptions _jwtOptions;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(UserManager<ApplicationUser> userManager, JwtOptions jwtOptions, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, ILogger<LoginHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.username);
            _logger.LogInformation("{request.username} is trying to login", request.username);
            if (user == null)
            {
                _logger.LogWarning("Username {request.username} is not registered", request.username);
                user = await _userManager.FindByEmailAsync(request.username);
                if (user == null)
                {
                    _logger.LogWarning("Email {request.email} is not registered", request.username);
                    return new Result<string>(false, "User not found");
                }
            }

            if (!user.Active)
            {
                _logger.LogWarning("{request.username} is not active", request.username);
                return new Result<string>(false, "User is not active");
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, request.password, false, true);
            if (signInResult.Succeeded)
            {
                _logger.LogInformation("{request.username} has logged in", request.username);
                var jwt = await GenerateJwt(user);
                return new Result<string>(jwt, true, "User has logged in");
            }
            return new Result<string>(false, "User not found");
        }

        private async Task<string> GenerateJwt(ApplicationUser user)
        {
            _logger.LogInformation($"Generating JWT for user {user.UserName}");
            var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

            var claims = await _userManager.GetClaimsAsync(user);
            foreach (var item in claims)
            {
                authClaims.Add(item);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRoleName in userRoles)
            {
                var userRole = await _roleManager.FindByNameAsync(userRoleName);
                var listOfClaims = await _roleManager.GetClaimsAsync(userRole);

                foreach (var item in listOfClaims)
                {
                    authClaims.Add(item);
                }
            }

            authClaims.Add(new Claim("FullName", user.FullName));
            authClaims.Add(new Claim("IsStaff", user.Staff.ToString()));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var lifeTime = _jwtOptions.ValidateLifetime ? _jwtOptions.Lifetime : 60 * 24 * 365;

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.ValidateIssuer ? _jwtOptions.ValidIssuer : null,
                audience: _jwtOptions.ValidateAudience ? _jwtOptions.ValidAudience : null,
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(lifeTime),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
