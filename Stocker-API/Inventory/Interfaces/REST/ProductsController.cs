using Stocker_API.Inventory.Domain.Model.Queries;
using Stocker_API.Inventory.Domain.Services;
using Stocker_API.Inventory.Interfaces.REST.Resources;
using Stocker_API.Inventory.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Stocker_API.Inventory.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductsController(
    IProductCommandService productCommandService,
    IProductQueryService productQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource createProductResource)
    {
        var createProductCommand =
            CreateProductCommandFromResourceAssembler.ToCommandFromResource(createProductResource);
        var product = await productCommandService.Handle(createProductCommand);
        if (product is null) return BadRequest();
        var resource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return CreatedAtAction(nameof(GetProductById), new { productId = resource.Id }, resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var getAllProductsQuery = new GetAllProductsQuery();
        var products = await productQueryService.Handle(getAllProductsQuery);
        var resources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById([FromRoute] int productId)
    {
        var product = await productQueryService.Handle(new GetProductByIdQuery(productId));
        if (product == null) return NotFound();
        var resource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return Ok(resource);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProductById([FromBody] ProductResource productResource)
    {
        var getProductByIdQuery = new GetProductByIdQuery(productResource.Id);
        var product = await productQueryService.Handle(getProductByIdQuery);
        product.Name = productResource.Name;
        product.Description = productResource.Description;
        product.CategoryId = productResource.Category.Id;
        product.PhotoUrl = productResource.PhotoUrl;
        product.PurchasePrice = productResource.PurchasePrice;
        product.SalePrice = productResource.SalePrice;
        product.Stock = productResource.Stock;
        product.ExpiryDate = productResource.ExpiryDate;
        await productCommandService.Update(product);
        return Ok(product);
    }
    
    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var getProductByIdQuery = new GetProductByIdQuery(productId);
        var product = await productQueryService.Handle(getProductByIdQuery);
        await productCommandService.Delete(product);
        return Ok(product);
    }
}