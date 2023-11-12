using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Api.Entities;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Title { get; set; }

    [Required] [MaxLength(200)] public string Content { get; set; }

    [Required] [MaxLength(50)] public string UserName { get; set; }
    [Required] public DateTime CreationDate { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public Post(string title, string content)
    {
        Title = title;
        Content = content;
        CreationDate = DateTime.Now;
    }
}