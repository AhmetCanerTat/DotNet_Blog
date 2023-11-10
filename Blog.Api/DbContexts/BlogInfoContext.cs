using Blog.Api.Entities;

namespace Blog.Api.DbContexts;
using Microsoft.EntityFrameworkCore;

public class BlogInfoContext : DbContext
{
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    
    public BlogInfoContext(DbContextOptions<BlogInfoContext> options) : base(options)
    {
        
    }
}