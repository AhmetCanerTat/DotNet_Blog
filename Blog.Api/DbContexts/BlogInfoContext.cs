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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().Navigation(x => x.Comments).AutoInclude();
        modelBuilder.Entity<Post>().HasData(
            new Post("First Post", "This is the first post")
            {
                Id = 1,
                CreationDate = DateTime.Now,
            },
            new Post("Second Post", "This is the second post")
            {
                Id = 2,
                CreationDate = DateTime.Now,
            },
            new Post("Third Post", "This is the third post")
            {
                Id = 3,
                CreationDate = DateTime.Now,
            });
        modelBuilder.Entity<Comment>().HasData(
            new Comment("First Comment", "This is the first comment")
            {
                Id = 1,
                CreationDate = DateTime.Now,
                PostId = 1
            }, 
            new Comment("Second Comment", "This is the second comment")
            {
                Id = 2,
                CreationDate = DateTime.Now,
                PostId = 1
            },new Comment("First Comment", "This is the first comment")
            {
                Id = 3,
                CreationDate = DateTime.Now,
                PostId = 2
            }, new Comment("Second Comment", "This is the second comment")
            {
                Id = 4,
                CreationDate = DateTime.Now,
                PostId = 2
            },new Comment("First Comment", "This is the first comment")
            {
                Id = 5,
                CreationDate = DateTime.Now,
                PostId = 3
            }, new Comment("Second Comment", "This is the second comment")
            {
                Id = 6,
                CreationDate = DateTime.Now,
                PostId = 3
            });
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>().Navigation(x=>x.Comments).AutoInclude();
    }
}