using System.ComponentModel.DataAnnotations;
using Blog.Api.Entities;

namespace Blog.Api.Models;

public class PostDto
{
    public PostDto()
    {
    }

    public PostDto(Post post)
    {
        Id = post.Id;
        Title = post.Title;
        Content = post.Content;
        FirstName = post.FirstName;
        LastName = post.LastName;
        CreationDate = post.CreationDate;
        Comments = post.Comments.Select(com => new CommentDto(com)).ToList();
    }

    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
}