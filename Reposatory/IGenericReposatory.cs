using System.Linq.Expressions;

namespace Shopy.Reposatory;

public interface IGenericReposatory <T> where T : class
{
    //.include("").tolist()
    //.where (x=>x.Id == Id).to list()
    IEnumerable<T> GetAll(Expression<Func<T,bool>>? predicate=null, string? includeWord=null);
    T GetFirstOrDesault(Expression<Func<T,bool>>? predicate=null, string? includeWord=null);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}