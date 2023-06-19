using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        // For generic ListAsync method (that get all products with its type and brand)
        // to add include method to its Iqueryable data.
        public ProductsWithTypesAndBrandsSpecification(string sort)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
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