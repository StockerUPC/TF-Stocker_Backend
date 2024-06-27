using Stocker_API.Purchases.Domain.Model.Queries;
using Stocker_API.Purchases.Domain.Services;
using Stocker_API.Purchases.Interfaces.REST.Resources;
using Stocker_API.Purchases.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Stocker_API.Purchases.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class PurchasesController(
    IPurchaseCommandService purchaseCommandService,
    IPurchaseQueryService purchaseQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePurchase([FromBody] CreatePurchaseResource createPurchaseResource)
    {
        var createPurchaseCommand =
            CreatePurchaseCommandFromResourceAssembler.ToCommandFromResource(createPurchaseResource);
        var purchase = await purchaseCommandService.Handle(createPurchaseCommand);
        if (purchase is null) return BadRequest();
        var resource = PurchaseResourceFromEntityAssembler.ToResourceFromEntity(purchase);
        return CreatedAtAction(nameof(GetPurchaseById), new { purchaseId = resource.Id }, resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPurchases()
    {
        var getAllPurchasesQuery = new GetAllPurchasesQuery();
        var purchases = await purchaseQueryService.Handle(getAllPurchasesQuery);
        var resources = purchases.Select(PurchaseResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{purchaseId}")]
    public async Task<IActionResult> GetPurchaseById([FromRoute] int purchaseId)
    {
        var purchase = await purchaseQueryService.Handle(new GetPurchaseByIdQuery(purchaseId));
        if (purchase == null) return NotFound();
        var resource = PurchaseResourceFromEntityAssembler.ToResourceFromEntity(purchase);
        return Ok(resource);
    }
    [HttpDelete("{purchaseId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a Purchase by id",
        Description = "Deletes a Purchase for a given identifier",
        OperationId = "DeletePurchaseById")]
    [SwaggerResponse(200, "The Purchase was found", typeof(PurchaseResource))]
    public async Task<IActionResult> DeletePurchaseById(int purchaseId)
    {
        var getPurchaseByIdQuery = new GetPurchaseByIdQuery(purchaseId);
        var purchase = await purchaseQueryService.Handle(getPurchaseByIdQuery);
        await purchaseCommandService.Delete(purchase);
        return Ok(purchase);
    }
    
    [HttpPut("{purchaseId:int}")]
    [SwaggerOperation(
        Summary = "Updates a Purchase by id",
        Description = "Updates a Purchase for a given identifier",
        OperationId = "UpdatePurchaseById")]
    [SwaggerResponse(200, "The Purchase was found", typeof(PurchaseResource))]
    public async Task<IActionResult> UpdatePurchaseById(int purchaseId, [FromBody] PurchaseResource purchaseResource)
    {
        var getPurchaseByIdQuery = new GetPurchaseByIdQuery(purchaseId);
        var purchase = await purchaseQueryService.Handle(getPurchaseByIdQuery);
        purchase.SupplierId = purchaseResource.Supplier.Id;
        purchase.TotalAmount = purchaseResource.TotalAmount;
        await purchaseCommandService.Update(purchase);
        return Ok(purchase);
    }
}