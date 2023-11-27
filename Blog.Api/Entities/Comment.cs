using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Blog.Api.Entities;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Title { get; set; }

    [Required] [MaxLength(200)] public string Content { get; set; }

    [ForeignKey("PostId")] public Post? Post { get; set; }
    public int PostId { get; set; }
    [ForeignKey("UserId")]
    public string? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }


    public DateTime CreationDate { get; set; }

    public Comment(string title, string content)
    {
        Title = title;
        Content = content;
        CreationDate = DateTime.Now;
    }
}