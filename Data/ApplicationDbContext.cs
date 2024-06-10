using Microsoft.EntityFrameworkCore;
using Shopy.Models;

namespace Shopy.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options){ }
    public DbSet<Category> Categories { get; set; }
}