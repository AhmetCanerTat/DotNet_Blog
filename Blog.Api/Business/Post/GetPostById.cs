using Blog.Api.DbContexts;
using Blog.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace Blog.Api.Business.Post;

public record GetPostById(int Id ): IQuery<PostDto>
{
    public class Handler:IRequestHandler<GetPostById, PostDto>
    {
        private readonly BlogInfoContext _context;

        public Handler(BlogInfoContext context) => this._context = context;

        public async Task<PostDto> Handle(GetPostById request, CancellationToken cancellationToken) =>
            new PostDto(await _context.Posts.SingleRequiredAsync(x => x.Id == request.Id, cancellationToken));
    }
    
}