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
    
   
}