using System.Linq.Expressions;

namespace Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() { }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; } // Where clause

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>(); // Include clause

        public Expression<Func<T, object>> OrderBy { get; private set; } // set in this class, OrderBy clause

        public Expression<Func<T, object>> OrderByDescending { get; private set; } // set in this class, OrderByDescending clause

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}