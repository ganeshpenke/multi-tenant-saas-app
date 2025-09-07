using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiTenantSaaS.Application.Products.Commands;

namespace MultiTenantSaaS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { ProductId = id });
        }
    }
}
