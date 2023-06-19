using Core.Entities;
using Core.Specification;

namespace Core.Contract.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        // Specification in place for adding Include method to generic Iqueryable data.
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        // Specification in place for counting the number of items return from data.
        Task<int> CountAsync(ISpecification<T> spec);
    }
}