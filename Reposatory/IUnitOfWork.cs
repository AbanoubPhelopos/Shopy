namespace Shopy.Reposatory;

public interface IUnitOfWork:IDisposable
{ 
    ICategoryReposatory Category { get; }
    int Complite();
}