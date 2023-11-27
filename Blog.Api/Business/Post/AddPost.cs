using Blog.Api.DbContexts;
using Blog.Api.Entities;
using Blog.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Blog.Api.Business.Post;

public record AddPost(string Title, string? Content, string UserId) : ICommand<PostDto>
{
    public class Handler : IRequestHandler<AddPost, PostDto>
    {
        private readonly BlogInfoContext _context;

        public Handler(BlogInfoContext context) => this._context = context;

        public async Task<PostDto> Handle(AddPost request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleRequiredAsync(x => x.Id == request.UserId, CancellationToken.None);
            var post = new Entities.Post(request.Title, request.Content!)
            {
                CreationDate = DateTime.Now,
                UserId = request.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync(cancellationToken);
            return new PostDto(post);
        }
    }
}