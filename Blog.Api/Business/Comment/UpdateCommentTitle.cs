using Blog.Api.DbContexts;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace Blog.Api.Business.Comment;

public record UpdateCommentTitle(int PostId, int CommentId, string Title, string UserId) : ICommand
{
    public class Handler : IRequestHandler<UpdateCommentTitle>
    {
        private readonly BlogInfoContext _context;

        public Handler(BlogInfoContext context) => this._context = context;

        public async Task<Unit> Handle(UpdateCommentTitle request, CancellationToken cancellationToken)
        {
            var com = await _context.Comments.SingleRequiredAsync(
                x => (x.Id == request.CommentId && x.PostId == request.PostId) && x.UserId == request.UserId,
                cancellationToken);
            com.Title = request.Title;
            await _context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}