using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Model.Queries;

namespace Stocker_API.Purchases.Domain.Services;

public interface IPurchaseQueryService
{
    Task<Purchase?> Handle(GetPurchaseByIdQuery query);
    Task<IEnumerable<Purchase>> Handle(GetAllPurchasesQuery query);
    Task<IEnumerable<Purchase>> Handle(GetAllPurchasesBySupplierIdQuery query);
}