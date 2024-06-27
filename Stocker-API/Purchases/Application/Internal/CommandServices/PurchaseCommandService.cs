using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Model.Commands;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Purchases.Domain.Services;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Purchases.Application.Internal.CommandServices;

public class PurchaseCommandService(IPurchaseRepository purchaseRepository, ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
    : IPurchaseCommandService
{
    
    public async Task<Purchase?> Handle(CreatePurchaseCommand command)
    {
        var purchase = new Purchase(command.SupplierId, command.TotalAmount);
        await purchaseRepository.AddAsync(purchase);
        await unitOfWork.CompleteAsync();
        var supplier = await supplierRepository.FindByIdAsync(command.SupplierId);
        purchase.Supplier = supplier;
        return purchase;
    }   
    
    public async Task<Purchase?> Delete(Purchase purchase)
    {
        purchaseRepository.Remove(purchase);
        await unitOfWork.CompleteAsync();
        return purchase;
    }
    
    public async Task<Purchase?> Update(Purchase purchase)
    {
        purchaseRepository.Update(purchase);
        await unitOfWork.CompleteAsync();
        return purchase;
    }
}