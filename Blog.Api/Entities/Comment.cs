using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public string UserName { get; set; }
    public DateTime CreationDate { get; set; }

    public Comment(string title, string content)
    {
        Title = title;
        Content = content;
        CreationDate = DateTime.Now;
    }
}