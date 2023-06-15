using System.Linq.Expressions;

namespace Core.Contract.Repository
{
    public interface IRepository<T>
    {
         IReadOnlyList<T> ListAllAsync();

         // Generic Expression: 
         //     x => x.Name.Contains("red");
         IReadOnlyList<T> FindAsync(Expression<Func<T, bool>> query);

         T GetByID(int id);
         void Add(T item);
         void Update(T item);
         void Delete(T item);
    }
}