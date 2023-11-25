using Blog.Api.DbContexts;
using Blog.Api.Models;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace Blog.Api.Business.Post;

public record AddPost(string Title, string? Content) : ICommand<PostDto>
{
    public class Handler : IRequestHandler<AddPost, PostDto>
    {
        private readonly BlogInfoContext context;

        public Handler(BlogInfoContext context) => this.context = context;

        public async Task<PostDto> Handle(AddPost request, CancellationToken cancellationToken)
        {
            var post = new Entities.Post(request.Title, request.Content!)
            {
                CreationDate = DateTime.Now
            };
            context.Posts.Add(post);
            await context.SaveChangesAsync(cancellationToken);
            return new PostDto(post);
        }
    }
}