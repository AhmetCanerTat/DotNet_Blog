using Blog.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Blog.Api.DbContexts;

using Microsoft.EntityFrameworkCore;

public class BlogInfoContext : IdentityDbContext<User>
{
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    public BlogInfoContext(DbContextOptions<BlogInfoContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.Entity<Post>().Navigation(x => x.Comments).AutoInclude();
      
 
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>().Navigation(x=>x.Comments).AutoInclude();
    }
}