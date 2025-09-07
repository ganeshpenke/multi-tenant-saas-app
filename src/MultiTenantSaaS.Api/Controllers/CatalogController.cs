using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiTenantSaaS.Application.DTOs;
using MultiTenantSaaS.Application.Tenants.Commands;

namespace MultiTenantSaaS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ISender _mediator;

        public CatalogController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantDto dto)
        {
            var tenant = await _mediator.Send(new CreateTenantCommand(dto));
            return Ok(new { tenant.Id, tenant.TenantName, tenant.Status, tenant.DatabaseName });
        }
    }
}
