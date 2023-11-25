using Blog.Api.DbContexts;
using Blog.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace Blog.Api.Business.Comment;

public record GetComment(int PostId, int CommentId) : IQuery<CommentDto>
{
    public class Handler : IRequestHandler<GetComment, CommentDto>
    {
        private readonly BlogInfoContext _context;
        public Handler(BlogInfoContext context) => this._context = context;
        
        public async Task<CommentDto> Handle(GetComment request, CancellationToken cancellationToken) =>
            new CommentDto(await _context.Comments.SingleRequiredAsync(x => x.Id == request.CommentId && x.PostId == request.PostId, cancellationToken));
    }
}