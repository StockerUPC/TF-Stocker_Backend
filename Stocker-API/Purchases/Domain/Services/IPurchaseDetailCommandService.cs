using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Model.Commands;

namespace Stocker_API.Purchases.Domain.Services;

public interface IPurchaseDetailCommandService
{
    Task<PurchaseDetail?> Handle(CreatePurchaseDetailCommand command);
    Task<PurchaseDetail?>Delete(PurchaseDetail purchaseDetail);
    Task<PurchaseDetail?>Update(PurchaseDetail purchaseDetail);
}