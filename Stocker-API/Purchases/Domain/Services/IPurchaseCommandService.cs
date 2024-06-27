using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Model.Commands;

namespace Stocker_API.Purchases.Domain.Services;

public interface IPurchaseCommandService
{
    Task<Purchase?> Handle(CreatePurchaseCommand command);
    Task<Purchase?>Delete(Purchase purchase);
    Task<Purchase?> Update(Purchase purchase);
}