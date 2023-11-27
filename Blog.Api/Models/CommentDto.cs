using Blog.Api.Entities;

namespace Blog.Api.Models;

public class CommentDto
{
    public CommentDto()
    {
    }

    public CommentDto(Comment comment)
    {
        Id = comment.Id;
        Title = comment.Title;
        Content = comment.Content;
        FirstName = comment.FirstName;
        LastName = comment.LastName;
        CreationDate = comment.CreationDate;
    }

    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
}