using Blog.Api.DbContexts;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace Blog.Api.Business.Comment;

public record UpdateCommentContent(int PostId, int CommentId, string Content) : ICommand
{
    public class Handler : IRequestHandler<UpdateCommentContent>
    {
        private readonly BlogInfoContext _context;

        public Handler(BlogInfoContext context) => this._context = context;

        public async Task<Unit> Handle(UpdateCommentContent request, CancellationToken cancellationToken)
        {
            var com = await _context.Comments.SingleRequiredAsync(x => x.Id == request.CommentId && x.PostId == request.PostId, cancellationToken);
            com.Content = request.Content;
            await _context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}