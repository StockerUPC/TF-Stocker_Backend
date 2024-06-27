using Stocker_API.Purchases.Domain.Model.Aggregates;
using Stocker_API.Purchases.Domain.Model.Commands;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Purchases.Domain.Services;
using Stocker_API.Shared.Domain.Repositories;

namespace Stocker_API.Purchases.Application.Internal.CommandServices;

public class PurchaseDetailCommandService(IPurchaseDetailRepository purchaseDetailRepository, IPurchaseRepository purchaseRepository, IUnitOfWork unitOfWork)
    : IPurchaseDetailCommandService
{
    
    public async Task<PurchaseDetail?> Handle(CreatePurchaseDetailCommand command)
    {
        var purchaseDetail = new PurchaseDetail(command.PurchaseId, command.ProductId, command.PurchasePrice,command.SalePrice, command.Quantity, command.Total);
        await purchaseDetailRepository.AddAsync(purchaseDetail);
        await unitOfWork.CompleteAsync();
        var purchase = await purchaseRepository.FindByIdAsync(command.PurchaseId);
        purchaseDetail.Purchase = purchase;
        return purchaseDetail;
    }   
    
    public async Task<PurchaseDetail?> Delete(PurchaseDetail purchaseDetail)
    {
        purchaseDetailRepository.Remove(purchaseDetail);
        await unitOfWork.CompleteAsync();
        return purchaseDetail;
    }
    
    public async Task<PurchaseDetail?> Update(PurchaseDetail purchaseDetail)
    {
        purchaseDetailRepository.Update(purchaseDetail);
        await unitOfWork.CompleteAsync();
        return purchaseDetail;
    }
}