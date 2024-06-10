using Microsoft.EntityFrameworkCore;
using Shopy.Models;

namespace Shopy.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options){ }
    private DbSet<Category> Categories { get; set; }
}