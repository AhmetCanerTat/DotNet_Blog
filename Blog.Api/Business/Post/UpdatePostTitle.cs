using Blog.Api.DbContexts;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Business.Post;

public record UpdatePostTitle(int PostId, string Title,string? UserId) : ICommand
{
    public class Handler : IRequestHandler<UpdatePostTitle>
    {
        private readonly BlogInfoContext _context;

        public Handler(BlogInfoContext context) => this._context = context;

        public async Task<Unit> Handle(UpdatePostTitle request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleRequiredAsync(x => x.Id == request.PostId && x.UserId == request.UserId, cancellationToken);
            post.Title = request.Title;
            await _context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}