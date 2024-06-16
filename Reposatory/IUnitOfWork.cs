namespace Shopy.Reposatory;

public interface IUnitOfWork:IDisposable
{ 
    ICategoryReposatory Category { get; }
    IProductReposatory Product { get; }

    int Complite();
}