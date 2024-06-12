using Shopy.Entities.Models;

namespace Shopy.Reposatory;

public interface ICategoryReposatory : IGenericReposatory<Category>
{
    void Update(Category category);
}