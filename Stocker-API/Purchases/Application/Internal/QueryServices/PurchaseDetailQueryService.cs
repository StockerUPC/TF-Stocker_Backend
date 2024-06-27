using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Model.Queries;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Purchases.Domain.Services;

namespace Stocker_API.Purchases.Application.Internal.QueryServices;

public class PurchaseDetailQueryService(IPurchaseDetailRepository purchaseDetailRepository) : IPurchaseDetailQueryService
{
    /**
     * <summary>
     *     This method is responsible for handling GetTutorialByIdentifierQuery
     * </summary>
     * <param name="query">GetTutorialByIdentifierQuery>Contains the Id of the Tutorial</param>
     * <returns>Tutorial - The Tutorial object</returns>
     */
    public async Task<PurchaseDetail?> Handle(GetPurchaseDetailByIdQuery query)
    {
        return await purchaseDetailRepository.FindByIdAsync(query.PurchaseDetailId);
    }

    /**
     * <summary>
     *     This method is responsible for handling GetAllTutorialsQuery
     * </summary>
     * <param name="query">GetAllTutorialsQuery</param>
     * <returns>IEnumerable of Tutorials - The list of Tutorial objects</returns>
     */
    public async Task<IEnumerable<PurchaseDetail>> Handle(GetAllPurchaseDetailsQuery query)
    {
        return await purchaseDetailRepository.ListAsync();
    }
    
    /**
     * <summary>
     *     This method is responsible for handling GetAllTutorialsByCategoryIdQuery
     * </summary>
     * <param name="query">GetAllTutorialsByCategoryIdQuery</param>
     * <returns>IEnumerable of Tutorials - The list of Tutorial objects</returns>
     */
    public async Task<IEnumerable<PurchaseDetail>> Handle(GetAllPurchaseDetailsByPurchaseIdQuery query)
    {
        return await purchaseDetailRepository.FindByPurchaseIdAsync(query.PurchaseId);
    }
}