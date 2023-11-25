using Blog.Api.DbContexts;
using Blog.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace Blog.Api.Business.Comment;

public record AddComment(string Title, string? Content, int PostId) : ICommand<CommentDto>
{
    public class Handler : IRequestHandler<AddComment, CommentDto>
    {
        private readonly BlogInfoContext context;

        public Handler(BlogInfoContext context) => this.context = context;

        public async Task<CommentDto> Handle(AddComment request, CancellationToken cancellationToken)
        {
            var post = await context.Posts.SingleRequiredAsync(x => x.Id == request.PostId, cancellationToken);
            var com = new Entities.Comment(request.Title, request.Content!)
            {
                CreationDate = DateTime.Now
            };
            post.Comments.Add(com);
            await context.SaveChangesAsync(cancellationToken);
            return new CommentDto(com);
        }
    }
}