using Microsoft.EntityFrameworkCore;
using Unicv.Streaming.Api.Data.Entities;

namespace Unicv.Streaming.Api.Data.Context;

public class DataContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public DbSet<Gender> Gender { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Actor> Actor { get; set; }
    public DbSet<Director> Director { get; set; }
    public DbSet<Profile> Profile { get; set; }
    public DbSet<Work> Work { get; set; }
    public DbSet<Rating> Rating { get; set; }
}
