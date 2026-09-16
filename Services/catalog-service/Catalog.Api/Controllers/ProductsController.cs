using Catalog.Api.Request;
using Catalog.Application.Commands;
using Catalog.Application.Dto;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
            return Ok(product);
        }
        [HttpGet("id/{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpGet("sku/{sku}")]
        public async Task<ActionResult<ProductDto>> GetProductBySku(string sku, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetProductBySkuQuery(sku));
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpGet("categoryId/{CategoryId:guid}")]
        public async Task<ActionResult<ProductDto>> GetProductsByCategoryId(Guid CategoryId, CancellationToken cancellationToken)
        {
            var products = await _mediator.Send(new GetProductsByCategoryQuery(CategoryId), cancellationToken);
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(request.Name, request.Price, request.Currency, request.ShortDescription, request.LongDescription, request.Sku, request.CategoryId);
            var productid = await _mediator.Send(command);
            return productid;
        }
        [HttpPut("price/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateProductPrice(Guid id,UpdateProductPriceRequest request,CancellationToken cancellationToken)
        {
            var updated = new UpdatePriceProductCommand(id, request.NewPrice, request.Currency);
            await _mediator.Send(updated);
            return NoContent();
        }

    }
}
