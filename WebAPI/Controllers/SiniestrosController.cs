using Application.Commands.siniestrosC;
using Application.DTO.Pagination;
using Application.DTO.Response;
using Application.Queries.SiniestrosQ;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiniestrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SiniestrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un siniestro.
        /// </summary>
        /// <param name="command">Informacion para crear el siniestro.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateSiniestrosCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { result }, null);
        }

        /// <summary>
        /// Obtiene todos los SiniestroResponse con filtros.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<SiniestroResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] int? departamentos_id, [FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(
                new GetSiniestrosQuery(departamentos_id, fechaInicio, fechaFin, page, pageSize));

            return Ok(result);

        }
    }
}
