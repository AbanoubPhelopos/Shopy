using Shopy.DataAccess.Data;
using Shopy.Entities.Models;

namespace Shopy.Reposatory;

public class CategoryReposatory : GenericReposatory<Category>,ICategoryReposatory
{
    private readonly ApplicationDbContext _context;
    public CategoryReposatory(ApplicationDbContext context):base(context)
    {
        _context = context;
    }


    public void Update(Category category)
    {
        var categoryFromDb = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
        if (categoryFromDb != null)
        {
            categoryFromDb.Name = category.Name;
            categoryFromDb.Description = category.Description;
            categoryFromDb.CreatedTime = DateTime.Now;
            _context.Update(categoryFromDb);
        }
        
    }
}