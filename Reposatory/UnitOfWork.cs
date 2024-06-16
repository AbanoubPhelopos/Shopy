using Shopy.DataAccess.Data;

namespace Shopy.Reposatory;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryReposatory Category { get; private set; }
    public IProductReposatory Product { get; private set; }
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Category = new CategoryReposatory(context);
        Product = new ProductRepopsatory(context);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public int Complite()
    {
        return _context.SaveChanges();
    }
}