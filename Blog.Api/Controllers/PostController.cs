using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Blog.Api.Business.Post;
using Blog.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;
    public PostController(IMediator mediator) => this._mediator = mediator;

    [HttpGet]
    public async Task<GetPosts.Result> GetPosts(string? title, string? searchQuery, int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _mediator.Send(new GetPosts(title, searchQuery, pageNumber, pageSize));
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.PaginationMetadata));
        return result;
    }

    [HttpGet("{postId}", Name = "GetPost")]
    public Task<PostDto> GetPostById(int postId) => _mediator.Send(new GetPostById(postId));

    public class PostCreationModel
    {
        [Required(ErrorMessage = "You should provide a title a value")]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(200)] public string? Content { get; set; }
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePostAsync([FromBody] PostCreationModel body)
    {
        var userId = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "UserId");

        var post = await _mediator.Send(new AddPost(body.Title, body.Content, userId.Value));
        return CreatedAtRoute("GetPost", new { postId = post.Id }, post);
    }

    public record UpdatePostTitleModel([MaxLength(50)] string Title);
    [Authorize]
    [HttpPost("{postId}/UpdateTitle")]
    public Task<Unit> UpdatePostTitle(int postId, [FromBody] UpdatePostTitleModel body)
    {
        var userId = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "UserId");
        return _mediator.Send(new UpdatePostTitle(postId, body.Title, userId.Value));
    }

    public record UpdatePostContentModel([MaxLength(200)] string Content);
    [Authorize]
    [HttpPost("{postId}/UpdateContent")]
    public Task<Unit> UpdatePostContent(int postId, [FromBody] UpdatePostContentModel body)
    {
        var userId = HttpContext.User.Identities.First().Claims.FirstOrDefault(x => x.Type == "UserId");
        return _mediator.Send(new UpdatePostContent(postId, body.Content, userId.Value));
    }
}