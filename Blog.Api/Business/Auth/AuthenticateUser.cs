using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Api.Entities;
using Fusonic.Extensions.MediatR;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Api.Business.Auth;

public record AuthenticateUser(string UserName, string Password) : ICommand<AuthenticateUser.Result>
{

    public record Result(bool Succesful, string? Token);

    public class Handler : IRequestHandler<AuthenticateUser, Result>
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public Handler(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<Result> Handle(AuthenticateUser request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            var authSuccesful = user != null && await userManager.CheckPasswordAsync(user, request.Password);

            string? token = null;

            if (authSuccesful)
            {
                var credentials = GetSigningCredentials();
                var claims = await GetClaims(user);
                var tokenOptions = GenerateTokenOptions(credentials, claims);
                token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            }

            return new Result(authSuccesful, token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = configuration.GetSection("jwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
        {
            var jwtSettings = configuration.GetSection("jwtConfig");
            return new JwtSecurityToken(
                issuer: jwtSettings["ValidIssuer"],
                audience: jwtSettings["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
                signingCredentials: credentials);
        }
    }
}