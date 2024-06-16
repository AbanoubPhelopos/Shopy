using Shopy.DataAccess.Data;
using Shopy.Entities.Models;

namespace Shopy.Reposatory;

public class ProductRepopsatory :GenericReposatory<Product>,IProductReposatory
    {
    private readonly ApplicationDbContext _context;
    public ProductRepopsatory(ApplicationDbContext context):base(context)
    {
        _context = context;
    }


    public void Update(Product product)
    {
        var productFromDb = _context.Products.FirstOrDefault(x => x.Id == product.Id);
        if (productFromDb != null)
        {
            productFromDb.Name = product.Name;
            productFromDb.Description = product.Description;
            productFromDb.Img = product.Img;
            productFromDb.Price = product.Price;
            
            _context.Update(productFromDb);
        }
        
    }
}