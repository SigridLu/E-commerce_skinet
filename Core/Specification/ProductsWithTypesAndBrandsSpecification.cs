using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
    public class   ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        // For generic ListAsync method (that get all products with its type and brand)
        // to add include method to its Iqueryable data.
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        // For generic GetEntityWithSpec method (that get a single product by id with its type and brand)
        // to add include method to its Iqueryable data.
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}