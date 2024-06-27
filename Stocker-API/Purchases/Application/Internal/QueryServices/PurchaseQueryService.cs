using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Model.Queries;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Purchases.Domain.Services;

namespace Stocker_API.Purchases.Application.Internal.QueryServices;

public class PurchaseQueryService(IPurchaseRepository purchaseRepository) : IPurchaseQueryService
{
    /**
     * <summary>
     *     This method is responsible for handling GetTutorialByIdentifierQuery
     * </summary>
     * <param name="query">GetTutorialByIdentifierQuery>Contains the Id of the Tutorial</param>
     * <returns>Tutorial - The Tutorial object</returns>
     */
    public async Task<Purchase?> Handle(GetPurchaseByIdQuery query)
    {
        return await purchaseRepository.FindByIdAsync(query.PurchaseId);
    }

    /**
     * <summary>
     *     This method is responsible for handling GetAllTutorialsQuery
     * </summary>
     * <param name="query">GetAllTutorialsQuery</param>
     * <returns>IEnumerable of Tutorials - The list of Tutorial objects</returns>
     */
    public async Task<IEnumerable<Purchase>> Handle(GetAllPurchasesQuery query)
    {
        return await purchaseRepository.ListAsync();
    }
    
    /**
     * <summary>
     *     This method is responsible for handling GetAllTutorialsByCategoryIdQuery
     * </summary>
     * <param name="query">GetAllTutorialsByCategoryIdQuery</param>
     * <returns>IEnumerable of Tutorials - The list of Tutorial objects</returns>
     */
    public async Task<IEnumerable<Purchase>> Handle(GetAllPurchasesBySupplierIdQuery query)
    {
        return await purchaseRepository.FindBySupplierIdAsync(query.SupplierId);
    }
}