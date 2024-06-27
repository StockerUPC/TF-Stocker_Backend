using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Purchases.Domain.Repositories;

public interface IPurchaseDetailRepository : IBaseRepository<PurchaseDetail>
{
    Task<IEnumerable<PurchaseDetail>> FindByPurchaseIdAsync(int purchaseId);
}