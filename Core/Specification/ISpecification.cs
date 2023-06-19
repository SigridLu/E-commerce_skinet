using System.Linq.Expressions;

namespace Core.Specification
{
    public interface ISpecification<T>
    {
        // Filtering (where clasue)
         Expression<Func<T, bool>> Criteria { get; }
         List<Expression<Func<T, object>>> Includes { get; }
         
         // Sorting
         Expression<Func<T, object>> OrderBy { get; }
         Expression<Func<T, object>> OrderByDescending { get; }

         // Pagination
         int Take { get; }
         int Skip { get; }
         bool IsPaginationEnabled { get; }
    }
}