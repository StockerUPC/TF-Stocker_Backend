using System.Net.Mime;
using Stocker_API.Purchases.Domain.Model.Queries;
using Stocker_API.Purchases.Domain.Services;
using Stocker_API.Purchases.Interfaces.REST.Resources;
using Stocker_API.Purchases.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Stocker_API.Purchases.Interfaces.REST;

/**
 * Categories Controller
 * <summary>
 *     This controller is responsible for handling all the requests related to categories.
 * </summary>
 */
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class SuppliersController(
    ISupplierCommandService supplierCommandService,
    ISupplierQueryService supplierQueryService)
    : ControllerBase
{
    /**
     * Create Category.
     * <summary>
     *     This method is responsible for handling the request to create a new category.
     * </summary>
     * <param name="createCategoryResource">The resource containing the information to create a new category.</param>
     * <returns>The newly created category resource.</returns>
     */
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a Supplier",
        Description = "Creates a Supplier with a given name",
        OperationId = "CreateSupplier")]
    [SwaggerResponse(201, "The Supplier was created", typeof(SupplierResource))]
    public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierResource createSupplierResource)
    {
        var createClientCommand =
            CreateSupplierCommandFromResourceAssembler.ToCommandFromResource(createSupplierResource);
        var supplier = await supplierCommandService.Handle(createClientCommand);
        if (supplier is null) return BadRequest();
        var resource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);
        return CreatedAtAction(nameof(GetSupplierById), new { supplierId = resource.Id }, resource);
    }

    /**
     * Get Category By Id.
     * <summary>
     *     This method is responsible for handling the request to get a category by its id.
     * </summary>
     * <param name="categoryId">The category identifier.</param>
     * <returns>The category resource.</returns>
     */
    [HttpGet("{supplierId:int}")]
    [SwaggerOperation(
        Summary = "Gets a Supplier by id",
        Description = "Gets a Supplier for a given identifier",
        OperationId = "GetSupplierById")]
    [SwaggerResponse(200, "The Supplier was found", typeof(SupplierResource))]
    public async Task<IActionResult> GetSupplierById(int supplierId)
    {
        var getSupplierByIdQuery = new GetSupplierByIdQuery(supplierId);
        var supplier = await supplierQueryService.Handle(getSupplierByIdQuery);
        var resource = SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier);
        return Ok(resource);
    }

    /**
     * Get All Categories.
     * <summary>
     *     This method is responsible for handling the request to get all categories.
     * </summary>
     * <returns>The categories resources.</returns>
     */
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all Suppliers",
        Description = "Gets all Suppliers",
        OperationId = "GetAllSuppliers")]
    [SwaggerResponse(200, "The Suppliers were found", typeof(IEnumerable<SupplierResource>))]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var getAllSuppliersQuery = new GetAllSuppliersQuery();
        var suppliers = await supplierQueryService.Handle(getAllSuppliersQuery);
        var resources = suppliers.Select(SupplierResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    [HttpDelete("{supplierId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a Supplier by id",
        Description = "Deletes a Supplier for a given identifier",
        OperationId = "DeleteSupplierById")]
    [SwaggerResponse(200, "The Supplier was found", typeof(SupplierResource))]
    public async Task<IActionResult> DeleteSupplierById(int supplierId)
    {
        var getSupplierByIdQuery = new GetSupplierByIdQuery(supplierId);
        var supplier = await supplierQueryService.Handle(getSupplierByIdQuery);
        await supplierCommandService.Delete(supplier);
        return Ok(supplier);
    }
    
    [HttpPut("{supplierId:int}")]
    [SwaggerOperation(
        Summary = "Updates a Supplier by id",
        Description = "Updates a Supplier for a given identifier",
        OperationId = "UpdateSupplierById")]
    [SwaggerResponse(200, "The Supplier was found", typeof(SupplierResource))]
    public async Task<IActionResult> UpdateSupplierById(int supplierId, [FromBody] SupplierResource supplierResource)
    {
        var getSupplierByIdQuery = new GetSupplierByIdQuery(supplierId);
        var supplier = await supplierQueryService.Handle(getSupplierByIdQuery);
        supplier.Name = supplierResource.Name;
        supplier.Number = supplierResource.Number;
        supplier.Email = supplierResource.Email;
        await supplierCommandService.Update(supplier);
        return Ok(supplier);
    }
}