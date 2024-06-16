using Shopy.Entities.Models;

namespace Shopy.Reposatory;

public interface IProductReposatory: IGenericReposatory<Product>
{
    void Update(Product product);
}