using Application.DTO.Pagination;
using Application.Queries.Catalogo;
using Application.Queries.CiudadQ;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatalogoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene todos las ciudades.
        /// </summary>
        [HttpGet("ciudades")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountPage<ciudades>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountPage<ciudades>>> GetAllciudades(string? nombre,int PageNumber = 1, int PageSize = 10)
        {
            var query = new GetAllCiudadesQuery(nombre, PageNumber, PageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        /// <summary>
        /// Obtiene todos los departamentos.
        /// </summary>
        [HttpGet("departamentos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountPage<departamentos>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountPage<departamentos>>> GetAlldepartamentos(string? nombre,int PageNumber = 1, int PageSize = 10)
        {
            var query = new GetAllDepartamentosQuery(nombre, PageNumber, PageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        /// <summary>
        /// Obtiene todos los tipos_siniestro.
        /// </summary>
        [HttpGet("tipos_siniestro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountPage<tipos_siniestro>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountPage<tipos_siniestro>>> GetAlltipos_siniestro(string? nombre,int PageNumber = 1, int PageSize = 10)
        {
            var query = new GetAllTipo_siniestroQuery(nombre, PageNumber, PageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
