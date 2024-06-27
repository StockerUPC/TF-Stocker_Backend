using System.Net.Mime;
using Stocker_API.Purchases.Domain.Model.Queries;
using Stocker_API.Purchases.Domain.Services;
using Stocker_API.Purchases.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Stocker_API.Purchases.Interfaces.REST;

[ApiController]
[Route("/api/v1/suppliers/{supplierId}/Purchases")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Suppliers")]
public class SupplierPurchasesController(IPurchaseQueryService purchaseQueryService) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetPurchasesByClientId([FromRoute] int supplierId)
    {
        var getAllPurchasesBySupplierIdQuery = new GetAllPurchasesBySupplierIdQuery(supplierId);
        var purchases = await purchaseQueryService.Handle(getAllPurchasesBySupplierIdQuery);
        var resources = purchases.Select(PurchaseResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}