using Blog.Api.Business.Post;
using Blog.Api.DbContexts;
using Blog.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace Blog.Api.Business.Comment;

public record GetCommentsByPostId(int PostId) : IQuery<GetCommentsByPostId.Result>
{
    public record Result(IEnumerable<CommentDto> Items);

    public class Handler : IRequestHandler<GetCommentsByPostId, Result>
    {
        private readonly BlogInfoContext _context;
        public Handler (BlogInfoContext context) => this._context = context;

        public async Task<Result> Handle(GetCommentsByPostId request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleRequiredAsync(x => x.Id == request.PostId, cancellationToken);
            return new Result(post.Comments.Select(x => new CommentDto(x)));
        }
    }
   
}