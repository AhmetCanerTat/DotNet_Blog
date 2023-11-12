using Blog.Api.DbContexts;
using Blog.Api.Models;
using CityInfo.Api.Models;
using Fusonic.Extensions.MediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Business.Post;

public record GetPosts
    (string? Name = null, string? SearchQuery = null, int PageNumber = 1, int PageSize = 10) : IQuery<GetPosts.Result>
{
    public record Result(IEnumerable<PostDto> Items, PaginationMetadata PaginationMetadata);

    public class Handler : IRequestHandler<GetPosts, Result>
    {
        private readonly BlogInfoContext _context;

        public Handler(BlogInfoContext context) => this._context = context;

        public async Task<Result> Handle(GetPosts request, CancellationToken cancellationToken)
        {
            var quearyable = _context.Posts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                quearyable = quearyable.Where(x => x.Title == request.Name);
            }

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                quearyable = quearyable.Where(x =>
                    x.Title.Contains(request.SearchQuery) || x.Content!.Contains(request.SearchQuery));
            }

            var totalItemsCount = await quearyable.CountAsync(cancellationToken);

            var paginationMetadata = new PaginationMetadata(totalItemsCount, request.PageSize, request.PageNumber);

            return new Result(await quearyable.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize)
                .Select(x => new PostDto(x))
                .ToListAsync(cancellationToken), paginationMetadata);
        }
    }
}