using System.ComponentModel.DataAnnotations;
using Blog.Api.Business.Comment;
using Blog.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiController]
[Route("api/posts/{postId}/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;
    public CommentController(IMediator mediator) => this._mediator = mediator;

    [HttpGet]
    public Task<GetCommentsByPostId.Result> GetCommentsByPostId(int postId) =>
        _mediator.Send(new GetCommentsByPostId(postId));

    [HttpGet("{commentId}", Name = "GetComment")]
    public Task<CommentDto> GetComment(int postId, int commentId) =>
        _mediator.Send(new GetComment(postId, commentId));

    public class CommentCreationModel
    {
        [Required(ErrorMessage = "You should provide a title value.")]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(50)] public string? Content { get; set; }
    }


    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateCommentAsync([FromRoute] int postId,
        [FromBody] CommentCreationModel body)
    {
        var com = await _mediator.Send(new AddComment(body.Title, body.Content, postId));
        
        return CreatedAtRoute("GetComment", new
        {
            postId, commentId = com.Id
        }, com);
    }
}