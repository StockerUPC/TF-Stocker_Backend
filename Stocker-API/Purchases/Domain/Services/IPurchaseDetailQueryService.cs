using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Model.Queries;

namespace Stocker_API.Purchases.Domain.Services;

public interface IPurchaseDetailQueryService
{
    Task<PurchaseDetail?> Handle(GetPurchaseDetailByIdQuery query);
    Task<IEnumerable<PurchaseDetail>> Handle(GetAllPurchaseDetailsQuery query);
    Task<IEnumerable<PurchaseDetail>> Handle(GetAllPurchaseDetailsByPurchaseIdQuery query);
}