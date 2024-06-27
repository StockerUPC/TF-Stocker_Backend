using Stocker_API.Inventory.Domain.Model.Aggregates;
using Stocker_API.Inventory.Domain.Model.Queries;
using Stocker_API.Inventory.Domain.Repositories;
using Stocker_API.Inventory.Domain.Services;

namespace Stocker_API.Inventory.Application.Internal.QueryServices;

public class ProductQueryService(IProductRepository productRepository) : IProductQueryService
{
    /**
     * <summary>
     *     This method is responsible for handling GetTutorialByIdentifierQuery
     * </summary>
     * <param name="query">GetTutorialByIdentifierQuery>Contains the Id of the Tutorial</param>
     * <returns>Tutorial - The Tutorial object</returns>
     */
    public async Task<Product?> Handle(GetProductByIdQuery query)
    {
        return await productRepository.FindByIdAsync(query.ProductId);
    }

    /**
     * <summary>
     *     This method is responsible for handling GetAllTutorialsQuery
     * </summary>
     * <param name="query">GetAllTutorialsQuery</param>
     * <returns>IEnumerable of Tutorials - The list of Tutorial objects</returns>
     */
    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query)
    {
        return await productRepository.ListAsync();
    }
    
    /**
     * <summary>
     *     This method is responsible for handling GetAllTutorialsByCategoryIdQuery
     * </summary>
     * <param name="query">GetAllTutorialsByCategoryIdQuery</param>
     * <returns>IEnumerable of Tutorials - The list of Tutorial objects</returns>
     */
    public async Task<IEnumerable<Product>> Handle(GetAllProductsByCategoryIdQuery query)
    {
        return await productRepository.FindByCategoryIdAsync(query.CategoryId);
    }
}