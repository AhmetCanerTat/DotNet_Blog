using Blog.Api.DbContexts;
using Blog.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace Blog.Api.Business.Comment;

public record AddComment(string Title, string? Content, int PostId,string UserId) : ICommand<CommentDto>
{
    public class Handler : IRequestHandler<AddComment, CommentDto>
    {
        private readonly BlogInfoContext _context;

        public Handler(BlogInfoContext context) => this._context = context;

        public async Task<CommentDto> Handle(AddComment request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleRequiredAsync(x => x.Id == request.UserId, CancellationToken.None);

            var post = await _context.Posts.SingleRequiredAsync(x => x.Id == request.PostId, cancellationToken);
            var com = new Entities.Comment(request.Title, request.Content!)
            {
                UserId = request.UserId,
                CreationDate = DateTime.Now,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            post.Comments.Add(com);
            await _context.SaveChangesAsync(cancellationToken);
            return new CommentDto(com);
        }
    }
}