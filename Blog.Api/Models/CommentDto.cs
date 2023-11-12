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
        UserName = comment.UserName;
        CreationDate = comment.CreationDate;
    }


    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? UserName { get; set; }

    public DateTime CreationDate { get; set; }
}