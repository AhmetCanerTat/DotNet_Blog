using System.ComponentModel.DataAnnotations;
using Blog.Api.Entities;
using Fusonic.Extensions.MediatR;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blog.Api.Business.Auth;

public record RegisterUser(string? FirstName, string? LastName, [Required] string UserName,
    [Required] string Password, string? Email, string? PhoneNumber) : ICommand<IdentityResult>
{
    public class Handler : IRequestHandler<RegisterUser, IdentityResult>
    {
        private readonly UserManager<User> userManager;

        public Handler(UserManager<User> userManager) => this.userManager = userManager;

        public async Task<IdentityResult> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            return await userManager.CreateAsync(user, request.Password);
        }
    }
}