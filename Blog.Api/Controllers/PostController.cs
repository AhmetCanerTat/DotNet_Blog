using Blog.Api.Business.Post;
using Blog.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController :ControllerBase
{
    private readonly IMediator _mediator;
    public PostController(IMediator mediator) => this._mediator = mediator;
    
    [HttpGet]
    public async Task<GetPosts.Result> Posts(string? title, string? searchQuery, int pageNumber = 1,
        int pageSize = 10)
    {
        var result = await _mediator.Send(new GetPosts(title, searchQuery, pageNumber, pageSize));
        Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(result.PaginationMetadata));
        return result;
    }
    [HttpGet("{id}")]
    public async Task<PostDto> GetPostById(int id) => await _mediator.Send(new GetPostById(id));
}